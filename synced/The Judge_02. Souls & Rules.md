# 02. Souls & Rules

Confluence: https://malvum.atlassian.net/wiki//pages/viewpage.action?pageId=17334273

I have removed the "Difficulty Tiers" section as requested and replaced it with the **Dossier/Bio** structure. The logic sections remain faithful to the "One Strike" system we agreed upon.

* * *

# 02. The Desk: Souls & Rules

> 
> **Status:** 🚧 **IN DESIGN**
> 
> - **Core Mechanic:** "One Strike" System (Restrictive Logic).
> - **Data Structure:** Structured Bio/Dossier (Coherent Personas).
> - **Default State:** All Souls are **ALLOWED** unless a rule explicitly bans them.
> 

* * *

## 1. The Core Concept

The gameplay is a **compliance checklist**. You are not balancing good vs. evil. You are scanning for discrepancies.

- **The Default:** Every soul has the right to reincarnate.
- **The Task:** You are looking for **Discrepancies** (Reasons to Deny) based on today's rules.
- **The Logic:** If you find **ONE** reason to Deny, the soul is rejected. It does not matter if they have 10 other good traits. One strike = Out.

* * *

## 2. The Soul (The Dossier)

A Soul is not just a random sentence. It is a coherent **Identity Card** (Dossier). The difficulty comes from having to scan multiple biographical fields to find the hidden "Facts" buried within the personal history.

### A. Soul Anatomy

Every soul card follows a template of fields. The AI generates the entire card at once to ensure the personality is consistent (e.g., a "Rich" soul will have expensive hobbies).

**Primary Fields (Always Present):**

- **Personal Info:** Name, Age, Gender, Origin.
- **Cause of Death:** How they died (often ironic or telling).
- **Last Phrase:** The last thing they said or thought before passing.

**Variable Fields (Ideas for Context):**

- **Financial Status:** Income bracket, debts, or assets.
- **Hobbies/Habits:** How they spent their free time.
- **Relationships:** Marital status, pets, rivalries.
- **Notable Life Event:** A specific memory or achievement.
- **Inventory:** Items found in their pockets at death.

### B. Signal vs. Noise (The Search)

Most of the information on the card is **Flavor Text (Noise)** to flesh out the character. The **Rule-Triggering Traits (Signal)** are woven naturally into one of these fields.

Example: The "Line Cutter" Trait

This trait is not a standalone sentence. It is injected into a specific field depending on the soul's story.

- *Variation A (In Cause of Death):* "Stabbed in a nightclub while trying to skip the queue."
- *Variation B (In Hobbies):* "Enjoyed finding creative ways to bypass waiting lines at theme parks."
- *Variation C (In Last Phrase):* "I'm sure they won't mind if I just squeeze in here..."

* * *

## 3. The Rules (The Filter)

Rules are **Negative Filters**. They tell the player who to **STOP**.

### A. The "One Strike" Logic

There are no conflicting "Allow" rules.

1. **Scan The Bio:** Read through Age, Death, Hobbies, etc.
2. **Match Found?** Does *any* field contain the banned behavior?
3. **Action:** Sort into **VOID** (Deny) or **REINCARNATE** (Allow).

### B. Rule Types & Structure

We use three types of rules to escalate cognitive load.

#### Type 1: The Simple Ban

A direct check against a single Fact hidden in the text.

- **Logic:** `IF [Fact] == True THEN DENY`
- **Rule Text:** *"Deny all Line Cutters."*
- **Player Action:** Scan the bio fields for evidence of cutting lines $\rightarrow$ Deny.

#### Type 2: The Demographic Ban (God's Whim)

A direct check against a specific data field (Age, Finances, etc.).

- **Logic:** `IF [Age] < 30 THEN DENY`
- **Rule Text:** *"No one under 30 allowed today."*
- **Player Action:** Check the **Age** field. Simple numeric check.

#### Type 3: The Exception Rule (Advanced)

This adds an "Escape Clause" inside a ban. This forces the player to check **two** facts/fields.

- **Logic:** `IF [Bad_Fact] == True THEN DENY... UNLESS [Good_Fact] == True`
- **Rule Text:** *"Deny Violent souls... EXCEPT if they are also Generous."*
- **Player Action:**

    1. Found Violence in "Relationships"? $\rightarrow$ Strike 1.
    2. Check "Financials" or "Hobbies" fields.
    3. Is there evidence of Generosity? $\rightarrow$ **Safe.** Allow the soul.

* * *

## 4. The Interaction Loop (Example)

Here is how a player processes a full dossier in the mid-game.

**Scenario Setup:**

- **Active Rule 1:** Deny **Lazy** Souls.
- **Active Rule 2:** Deny **Violent** Souls (Except **Funny**).

**The Soul Dossier:**

| **Field** | **Content** | **Analysis** |
| --- | --- | --- |
| **Name** | John "Snooze" Doe | Flavor |
| **Age** | 42 | Flavor |
| **Cause of Death** | House fire (didn't change batteries in detector). | **SIGNAL FOUND (Lazy)** |
| **Last Phrase** | "Tell my wife... I hid the money in the..." | **SIGNAL FOUND (Funny)** |
| **Relationships** | Single. | Flavor |

**Player Thinking Process:**

1. **Scan Bio:** Reads Cause of Death. *"Didn't change batteries..."*

    - *Interpretation:* This implies **Lazy**.
    - *Check Rule 1:* Matches "Deny Lazy".
    - *Current Verdict:* **DENY**.
2. **Check Exceptions:** Reads Last Phrase. *"Classic fake-out joke."*

    - *Interpretation:* This implies **Funny**.
    - *Check Rule 1 Exception:* Does Rule 1 have an exception for Funny? **No.**
    - *Check Rule 2:* The soul isn't Violent, so Rule 2 is irrelevant.
3. **Final Decision:**

    - The soul is **Lazy**. Rule 1 says Deny Lazy. There is no exception for it.
    - **Action:** Sort to **VOID**.

* * *

## 5. Sample Data Dictionary

A small list of Traits we will use to build the prototype.

| **Fact ID** | **Category** | **Planet Impact** |
| --- | --- | --- |
| `LINE_CUTTER` | Petty Sin | Chaos (Minor) |
| `CART_ABANDONER` | Petty Sin | Chaos (Minor) |
| `VIOLENT` | Major Sin | Chaos (Major) |
| `LOUD_CHEWER` | Annoyance | Chaos (Minor) |
| `GENEROUS` | Virtue | Prosperity |
| `FUNNY` | Social | Stability |
| `RICH` | Status | Prosperity |
