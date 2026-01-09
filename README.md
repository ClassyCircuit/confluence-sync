# ConfluenceSync

A tiny console app to **one-way sync** a Confluence page tree into local **Markdown** files.

It is intentionally simple:
- Site/space/parent page are **hardcoded**.
- Auth comes from **environment variables**.
- Output is always **re-generated** (existing files are replaced).

This is meant to produce Markdown files you can later feed into an LLM for analysis.

## What it syncs (hardcoded)

- Confluence site: `https://malvum.atlassian.net/wiki`
- Space: `codingmuch` (personal space key: `~557058ff7ebda7b4834355a36a176a142ec712`)
- Parent page: `The Judge`
  - URL: `https://malvum.atlassian.net/wiki/spaces/~557058ff7ebda7b4834355a36a176a142ec712/pages/7405569/The+Judge`
  - Page ID: `7405569`

The app fetches:
- The parent page
- **All descendants** (children, grandchildren, etc.) recursively

## Output

All Markdown files are written to:

- `./synced/`

On each run, files in `./synced/` will be overwritten.

### File naming

The export is **flat** (no folders).

- Parent page: `The Judge.md`
- Child pages: `The Judge_<Child Title>.md`
- Deeper pages: `The Judge_<Parent Title>_<Child Title>.md` (full chain)

Titles are taken from Confluence and minimally normalized for filesystem safety.

## Requirements

- .NET SDK (this repo targets `net10.0`)
- A Confluence API token (Atlassian Cloud)

## Configuration (env vars)

Set these environment variables:

- `CONFL_EMAIL` — your Atlassian account email
- `CONFL_API_KEY` — your Confluence API token

Notes:
- Atlassian Cloud typically uses **Basic auth** where the username is your email and the password is the API token.

## Running

Logging goes to stdout with UTC timestamps, so you can see exactly where failures happen.

From the repository root:

```bash
export CONFL_EMAIL="you@example.com"
export CONFL_API_KEY="<your-api-token>"

dotnet run
```

After a successful run, `./synced/` will contain Markdown files for the parent page and all descendants.

## Tasks (checked off as implemented)

- [x] Call Confluence REST API to fetch the parent page.
- [x] Recursively enumerate all descendant pages.
- [x] Convert Confluence storage (XHTML) to Markdown.
- [x] Write Markdown files into `./synced/` (always overwritten).
- [x] Refactor into small SOLID classes.

## Code layout

- `AppConfig.cs` hardcoded site/space/root/output.
- `Env.cs` required env var helper.
- `ConfluenceRestClient.cs` minimal Confluence REST calls.
- `PageTreeWalker.cs` tree traversal (recursive/stack).
- `MarkdownConverter.cs` storage HTML → Markdown.
- `FilenameBuilder.cs` flat filename generation.
- `PageExporter.cs` writes `.md` files.
- `Program.cs` wiring/entrypoint.

## How it works

- Uses Confluence REST API endpoints:
  - `GET /rest/api/content/{id}?expand=space,body.storage`
  - `GET /rest/api/content/{id}/child/page?start=...&limit=...&expand=space,body.storage`
- Walks the page tree using a stack (depth-first-ish) and handles child pagination.
- Converts returned `body.storage.value` (XHTML) into Markdown using `ReverseMarkdown`.

## Non-goals

- Two-way sync
- Deletions mirrored back to Confluence
- Live watching / incremental sync
- Complex configuration (this is a purpose-built tool)
