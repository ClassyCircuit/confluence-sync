# 01. Game Loop

Confluence: https://malvum.atlassian.net/wiki//pages/viewpage.action?pageId=16875521

> 
> - **Structure:** Core loops (Day & Shift) are locked.
> - **Pacing:** Inspired by *Papers, Please*.
> - **[TBD]:** Exact win/loss conditions (e.g., is there a fixed day limit or is it endless?).
> 

### 1. The Macro Loop: The Daily Cycle

The game progresses in strict "Days." This loop provides the narrative rhythm and long-term stakes.

**The 4-Phase Day Structure:**

#### 1. Morning Briefing (The Visit)

Instead of a static text screen, **God (your Boss)** enters your office, coffee cup in hand, for a "performance review." He is eccentric, petty, and reactive.

- **The Rant:** God explains his current mood or bias, which dictates the day's rules.

    - *Example Day A:* "I hate the smell of eggs. It lingers. Ban anyone who eats eggs. I don't care if they are saints."
    - *Example Day B:* "Okay, bad news. Because we banned egg-eaters, people started making *furniture* out of excess eggs. It's gross. Let the men eat eggs again, but keep the ban on women. Let's see if that fixes the furniture issue."
- **The Handover:** He drops a stamped paper on your desk. This becomes your **Active Rule Sheet**.
- **Purpose:** Narrative context. It frames the "Rules" not as divine justice, but as the whims of a quirky manager.

#### 2. The Shift (The Grind)

This is the core gameplay phase (see *The Micro Loop* below).

- **Action:** Souls arrive in a continuous queue.
- **Goal:** Process as many as possible against the clock while following God's specific instructions.
- **End Condition:** The shift timer expires or the queue is cleared.

#### 3. Daily Review (The Score)

- **Performance:** A summary of Correct vs. Incorrect decisions.
- **Penalties:** God’s feedback on your efficiency.

    - *If Good:* "Nice work. The egg smell is gone."
    - *If Bad:* "I saw an egg-eater get through. Do you want to be reincarnated as a slug? Focus."
- **Purpose:** Immediate feedback on player skill.

#### 4. World Update (The Consequence)

- **Planet View:** Visual changes to the planet (fire, smog, egg-furniture structures) based on who was let in.
- **News Bulletins:** Headlines reflecting the specific traits allowed.

    - *Headline:* "Egg Furniture Trends plummet as consumption rises."
- **Purpose:** Long-term narrative feedback.

* * *

### 2. The Micro Loop: The Judging Process

This is the repeatable action loop that happens dozens of times inside a single "Shift."

**Step-by-Step Flow:**

1. **Soul Arrives:**

    - A new soul dossier slides onto the desk.
    - **Constraint:** The queue is always moving; being slow causes backlog.
2. **Inspect (The Information Fog):**

    - Player reads the Life Overview—short narrative text descriptions.
    - **The Challenge:** The player must scan for behaviors that match today's rules. The text is not a keyword list; it requires interpretation (e.g., *"Always left the cart in the middle of the lot"* = Shopping Cart Abandoner).
3. **Cross-Reference:**

    - Player glances at the Rule Sheet (always visible on the desk) to verify if the found behavior is currently banned or allowed.
4. **The Sort (The Action):**

    - Instead of stamping, the player must **physically sort** the dossier into one of two pneumatic chutes/bins on the desk.
    - **Right Chute:** **REINCARNATE** (Approve).
    - **Left Chute:** **VOID** (Deny).
    - **Constraint:** Once the paper hits the chute, it is sucked away instantly. There is no retrieval.
5. **Transition (No Immediate Feedback):**

    - The dossier vanishes with a mechanical *whoosh*.
    - **Crucial Change:** There is **NO** immediate green light or red buzzer. The player does not know if they made a mistake in the moment.
    - **Next Soul:** The desk is clear, and the next soul slides in immediately.

    *Design Note: By removing immediate feedback, we increase the tension. The player only realizes they misunderstood a rule during the **Daily Review** or when they see the **Planet** burning.*

* * *

### 3. Time Pressure & Constraints

The player cannot play "perfectly" by taking infinite time. Pressure is applied via:

- **The Queue:** Souls arrive at a defined pace.
- **The Quota:** (Optional/TBD) A minimum number of souls must be processed to avoid penalties.

**The Trade-off (What happens if you are too slow?)** We have three distinct design directions for how to handle backlog. We need to decide which one best fits our "Chaos" pillar.

> 
> **Option A: Resource Starvation (Current Baseline)**
> 
> - **Mechanic:** The shift ends at a fixed time. Any unprocessed souls are sent back to limbo.
> - **Consequence:** The planet does not get enough souls to maintain the birth rate. **Population Stability** drops significantly.
> - *Pros:* Logical and easy to balance.
> - *Cons:* Passive penalty. It feels like a stat punishment rather than a narrative event.
> 

> 
> **Option B: The Floodgate (Recommended) ⭐**
> 
> - **Mechanic:** At the end of the shift, God gets impatient. Any souls remaining in the queue are **Automatically Reincarnated** without judgment.
> - **Consequence:** You lose control. Dangerous souls (Violent, Lazy, etc.) flood the planet instantly. The next day's news is filled with chaos caused by "The Unvetted."
> - *Pros:* Fits the "Absurd Consequences" pillar perfectly. High risk/high reward. Creates funny narrative outcomes.
> - *Cons:* Might be too punishing if the player is very slow.
> 

> 
> **Option C: Mental Fatigue (Overtime)**
> 
> - **Mechanic:** The shift does not end until the queue is clear.
> - **Consequence:** As you go into "Overtime," the player character suffers mental fatigue. The UI degrades—text becomes blurry, contrast fades, and the "Information Fog" becomes thicker, making judgment physically harder.
> - *Pros:* Creates intense atmospheric tension.
> - *Cons:* "Death Spiral" mechanic (being slow makes you slower). Can lead to frustration rather than fun chaos.
> 

**The Consequence of Being Too Fast:**

- You miss details, make mistakes, and actively sort chaotic elements into the wrong bin (Order drops).

### 4. Relationship Between Loops

- **The Macro Loop** configures the difficulty (Rules) and shows the stakes (News).
- **The Micro Loop** executes the test.
- **Feedback:** Skill in the Micro Loop → Positive News in the Macro Loop.

* * *
