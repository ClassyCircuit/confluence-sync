# 07. Kickoff & Task List

Confluence: https://malvum.atlassian.net/wiki//pages/viewpage.action?pageId=18579475

Here is the final page: **07. Kickoff & Task List**.

This page translates the design documents into actionable steps for a small team (you and your wife). It prioritizes the "Vertical Slice"—building just enough to prove the game is fun.

* * *

# 07. Kickoff & Task List

> 
> **Status:** 🚀 **ACTIONABLE**
> 
> - **Goal:** Move from "Design" to "Playable Prototype" (Vertical Slice).
> - **Timeline:** Recommended 2-week Sprint.
> 

* * *

## Phase 1: The "Ugly" Prototype (Days 1-5)

**Objective:** Build the core mechanic with *zero* art. Use grey boxes and default Unity UI. Prove the "One Strike" logic is fun.

### 👩‍💻 Coding Tasks (Unity)

- [ ] **Project Setup:** Create Unity project (2D Core). Set up Git repository.
- [ ] **Data Structures:** Create C# Classes for `Soul` (with fields: Name, Bio, Facts) and `Rule`.
- [ ] **The Generator:** Write a script that generates a random Soul with 1 Fact (e.g., `VIOLENT: True`).
- [ ] **The Desk Logic:**

    - Create a simple UI Canvas with:

        - Text Box (Soul Bio).
        - Text Box (Current Rule: "No Violence").
        - Two Buttons: "Reincarnate" and "Void".
    - Write the logic: `IF button_pressed == correct_decision THEN Score++ ELSE Score--`.

### 📝 Content Tasks (JSON/AI)

- [ ] **Setup AI:** Create the "Soul Generator" prompt in ChatGPT.
- [ ] **Batch 1:** Generate 20 test souls (10 Violent, 10 Safe) and save as `TestSouls.json`.
- [ ] **Import:** Manually paste this JSON into your Unity Data Manager to ensure it loads.

* * *

## Phase 2: The "Feel" Prototype (Days 6-10)

**Objective:** Add the physical interactions. Stop clicking buttons; start dragging papers.

### 👩‍💻 Coding Tasks

- [ ] **Drag & Drop:** Implement mouse input to grab the Soul Card object.
- [ ] **The Chutes:** Create trigger zones on Left/Right of screen.

    - `OnTriggerEnter`: If Card enters zone $\rightarrow$ Destroy Card $\rightarrow$ Trigger Scoring Logic.
- [ ] **Tweening:** Add simple animations (Card sliding in, Card getting sucked into tube).

### 🎨 Art/Audio Tasks (Placeholder)

- [ ] **Audio:** Find a free sound effect for "Paper Slide" and "Pneumatic Whoosh."
- [ ] **Visuals:** Draw a rough sketch of the Desk layout (doesn't need to be final art) to figure out spacing.

* * *

## Phase 3: The Loop & God (Days 11-14)

**Objective:** Connect the sorting to the actual game loop.

### 👩‍💻 Coding Tasks

- [ ] **Day Cycle Manager:** Create a script that manages: `Briefing -> Shift (Timer) -> Review -> Planet`.
- [ ] **God's Briefing:** Create a UI panel where "God" (placeholder text) tells you the rule.
- [ ] **End of Day:** Create a calculation screen that shows: "You sorted X souls. Y were mistakes."

* * *

## 🛠️ Tools We Need

- **Engine:** Unity (2D).
- **Version Control:** GitHub or PlasticSCM (Crucial for 2 people).
- **Content:** ChatGPT/Claude (for generating text).
- **Data Validation:** [JSONLint.com](https://jsonlint.com/) (to fix AI errors).
- **Task Tracking:** Trello/Jira (or just this Confluence page).

* * *

## 🛑 What NOT to do yet

- **Do NOT** build the Planet Simulation visuals yet. (Just use text logs: "Order -5").
- **Do NOT** write hundreds of souls. (Just use the 20 test souls).
- **Do NOT** optimize performance.
- **Do NOT** make final art assets.

Success Criteria:

You have "won" this phase when you can:

1. Read a card.
2. Read a rule.
3. Drag the card to a chute.
4. Feel satisfied when it goes *whoosh*.
