# ConfluenceSync

A tiny console app to **one-way sync** a Confluence page tree into local **Markdown** files.

It is intentionally simple:
- Site/space/parent page are **hardcoded**.
- Auth comes from **environment variables**.
- Output is always **re-generated** (existing files are replaced).

This is meant to produce Markdown files you can later feed into an LLM for analysis.

## What it syncs (hardcoded)

- Confluence site: `https://malvum.atlassian.net/wiki`
- Space: `codingmuch`
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

- `confl_email` — your Atlassian account email
- `confl_api_key` — your Confluence API token

Notes:
- Atlassian Cloud typically uses **Basic auth** where the username is your email and the password is the API token.

## Running

From the repository root:

```bash
export confl_email="you@example.com"
export confl_api_key="<your-api-token>"

dotnet run
```

After a successful run, `./synced/` will contain Markdown files for the parent page and all descendants.

## How it works (implementation plan)

1. Call Confluence REST API to fetch the parent page content (storage/body) and metadata.
2. Recursively enumerate descendants.
   - Preferred approach: use Confluence’s endpoints for listing children, walking the tree until no more children.
3. Convert Confluence storage format (XHTML) to Markdown.
   - Use a popular, battle-tested HTML→Markdown converter for .NET.
4. Write files under `./synced/`, always replacing existing content.

## Non-goals

- Two-way sync
- Deletions mirrored back to Confluence
- Live watching / incremental sync
- Complex configuration (this is a purpose-built tool)
