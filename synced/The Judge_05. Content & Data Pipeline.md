# 05. Content & Data Pipeline

Confluence: https://malvum.atlassian.net/wiki//pages/viewpage.action?pageId=17432580

Here is the updated content for **Page 05: Content & Data Pipeline**.

I have completely rewritten the **JSON Schema** and **AI Prompts** to match the new **Dossier/Bio** design (removing the old "Tiered Text" system) and included the Demographic fields needed for the new rule types.

* * *

# 05. Content & Data Pipeline

> 
> **Status:** ✅ **TECHNICAL**
> 
> - **Purpose:** The "Factory Floor." Defines how Souls, Rules, and News are structured and imported.
> - **Role of AI:** AI is a *tool* to generate coherent Personas (Dossiers). This page defines the **Constraints** that AI must follow.
> - **Master Source:** This page is the "Source of Truth" for all IDs and Enums.
> 

* * *

## 1. The Master Tag Dictionary (Enums)

**CRITICAL:** These are the *only* IDs the code understands. If a Prompt generates a trait called `"loud_mouth"` but the code expects `"LOUD_TALKER"`, the game will break.

### A. Behavioral Traits (Hidden Facts)

These are the booleans used for logic checks (Rules).

| **Trait ID (Code)** | **Category** | **Description** | **Planet Impact** |
| --- | --- | --- | --- |
| `LINE_CUTTER` | Petty Sin | Cuts in line. | -2 Order |
| `CART_ABANDONER` | Petty Sin | Leaves carts. | -1 Order |
| `LOUD_CHEWER` | Annoyance | Eating noises. | -1 Stability |
| `VIOLENT` | Major Sin | Physical harm. | -5 Order |
| `GENEROUS` | Virtue | Helps others. | +3 Prosperity |
| `FUNNY` | Social | Jokes/Charisma. | +1 Stability |
| `RICH` | Status | Wealthy. | +2 Prosperity |
| `EGG_EATER` | Specific | Eats eggs. | 0 (Narrative) |

### B. Bio Fields (Visible Text)

These are the specific text slots on the Soul Card.

- `name` (String)
- `age` (Int)
- `gender` (Enum: M, F, NB)
- `cause_of_death` (String)
- `last_phrase` (String)
- `financial_status` (String)
- `hobbies` (String)
- `relationships` (String)

* * *

## 2. Data Schemas (JSON)

Unity expects data in this exact format.

### A. Soul Schema (The Dossier)

A Soul is a collection of logic (`facts`) and a coherent identity (`bio`).
wide760wide760

> 
> **Note:** In the example above, the `VIOLENT` trait is injected into `cause_of_death` and `relationships`. The `RICH` trait is injected into `financial_status`.

### B. News Headline Schema

Headlines trigger based on a count of specific traits allowed that day.
wide760wide760
* * *

## 3. Generation Prompts (The "Magic Spells")

Use these System Prompts in ChatGPT/Claude to generate content.

### Prompt: Soul Dossier Generator

> 
> System: You are a dark, bureaucratic writer for a video game. You create coherent character profiles for souls entering the afterlife.
> 
> Constraint: Output valid JSON only. Matches the schema provided.
> 
> Task: Create a profile for a soul with the following Traits: [Insert Traits, e.g., VIOLENT + RICH].
> 
> **Instructions:**
> 
> 1. **Consistency:** The bio fields must match the traits. If they are `RICH`, their hobbies and cause of death should reflect wealth.
> 2. **Subtlety:** Do not use the word "Violent" directly. Describe the *action* or *consequence* (e.g., "Knuckles bruised," "Bar fight").
> 3. **Inject the Trait:** Ensure the evidence for the trait appears in at least one field (e.g., Cause of Death or Last Phrase).
> 4. **Flavor:** Fill the remaining fields with irrelevant but atmospheric flavor text (Noise).
> 

### Prompt: News Headline Generator

> 
> System: You are a satirist writing news headlines for a chaotic city.
> 
> Task: Write 3 headlines that would occur if a city was filled with people who [Insert Trait Here].
> 
> Tone: Absurd, panic-inducing, or deadpan.

* * *

## 4. The Workflow (How to Import)

How we get data from the Chatbot into the Game.

1. **Generate:** Run the Prompt in LLM (e.g., "Generate 10 souls that are VIOLENT").
2. **Validate:** Copy the JSON output into a JSON Validator (e.g., JSONLint) to ensure braces and commas are correct.
3. **Save:** Save as `Souls_Batch_01.json`.
4. **Import:** Place the `.json` file into `Assets/StreamingAssets/Data/Souls`.
5. **Test:** Run the "DataDebug" scene in Unity.

    - *Check:* Does the code recognize "Marcus Vanderbilt"?
    - *Check:* Does the code flag him as `VIOLENT` correctly?
