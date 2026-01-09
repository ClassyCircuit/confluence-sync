# 03. Consequences

Confluence: https://malvum.atlassian.net/wiki//pages/viewpage.action?pageId=17661953

# Planet & Progression

> 
> - **Role:** The "Scoreboard" for the player's long-term performance.
> - **Mechanism:** Deterministic feedback based on allowed Souls.
> - **[TBD]:** Visual implementation of the Planet View (Window vs. Cutscene).
> - **[TBD]:** Final Metric tuning.
> 

* * *

## 1. The Core Concept

The Planet is not a complex strategy simulation (like *Civilization*). It is a **Visual Feedback Loop**.

- **The Logic:** Every soul you sort into the **REINCARNATE** chute enters the simulation and affects specific metrics.
- **The Result:** If you allow too many flawed souls, the planet visibly decays. If you curate well, it thrives.
- **The Tone:** Changes are often immediate and absurd (e.g., allow one pyromaniac -&gt; a building catches fire).

* * *

## 2. Planet Metrics (The Scoreboard)

We need 3 core metrics to track the state of the world. These determine the visuals and the "End Game" state.

These metrics will be hidden to the player and will be used internally to decide how the planet changes visually and which news headlines to show.

Player will see one aggregated metric.

### Recommended Combination

This trio covers the three ways a society can fail: Violence, Poverty, and Extinction.

| **Metric** | **Why this Metric?** | **Low State (0-30)** | **High State (70-100)** |
| --- | --- | --- | --- |
| **ORDER** | **Tracks Safety.** Necessary because many "Sins" are disruptive (Violence, Rule Breaking, Noise). | **Anarchy.** Fires, riots, sirens, broken windows. | **Police State.** Sterile, patrol drones, eerie silence. |
| **PROSPERITY** | **Tracks Quality of Life.** Necessary to track economic traits (Lazy vs. Hardworking, Rich vs. Poor). | **Ruins.** Slums, dirt, unpainted grey concrete. | **Utopia.** Gold/Glass towers, holograms, greenery. |
| **POPULATION** | **Tracks Quantity.** Necessary to punish the player for banning *everyone* (being too strict). | **Ghost Town.** Empty streets, lights off, tumbleweeds. | **Gridlock.** Crowds, heavy traffic, smog. |

Metrics need to be more interesting and the **high state should be good for all,** the middle ground shouldn’t be the best scenario. So the scale is: 0 - bad, 50 - neutral, 100 - good, 

### **Ideas for metrics:**

Joy, Logic, Hygiene, Obesity, Liveliness, Entanglement, Air breathability, Alertness, Cleanliness, Prosperity, Privacy, Safety, Honesty

We should have probably 2 -3 rules related yo each stat

* * *

## 3. Visualizing the Planet (Options)

We need to decide *when* and *how* the player sees the consequences of their actions.

> 
> **Option A: The Window (Always Visible)**
> 
> - **Concept:** A large window behind the desk shows the city in real-time.
> - **Pros:** High immersion. Immediate feedback (Stamp "Arsonist" -&gt; See explosion in window).
> - **Cons:** Distracting during gameplay. technically expensive to render a 3D city while also rendering UI/Papers.
> - **Verdict:** Risky for production budget.
> 

> 
> **Option B: The End-of-Day Commute (Recommended) ⭐**
> 
> - **Concept:** You only see the planet **after the shift ends**. The camera pans up/out, or we see a "Train Ride Home" cutscene showing the city state.
> - **Pros:** Creates anticipation ("I hope I didn't break anything"). easier to optimize (static scene). Separates "Work" mode from "Result" mode.
> - **Cons:** No instant feedback on individual soul decisions.
> 

> 
> **Option C: The Monitor (Picture-in-Picture)**
> 
> - **Concept:** A small grainy CRT monitor on the desk shows a live feed of the planet.
> - **Pros:** Cheap to render (low res). Fits the "Bureaucracy" vibe.
> - **Cons:** Hard to see cool details on a tiny screen.
> 

* * *

## 4. The News System (Headlines)

Between days, the player sees a newspaper summary. This is the primary narrative reward.

A. How Headlines Work

Headlines are triggered by Counters. The game tracks how many of a specific trait you allowed that day.

B. Example Headlines

We map small behavioral flaws to massive societal problems.

| **Trait Allowed** | **Threshold** | **Headline Result** | **Impact** |
| --- | --- | --- | --- |
| `CART_ABANDONER` | &gt; 5 | *"Parking Lot Gridlock Paralyses National Economy"* | **-5 Prosperity** |
| `LOUD_CHEWER` | &gt; 3 | *"Misophonia Epidemic: Restaurants Ban All Food"* | **-2 Order** |
| `LINE_CUTTER` | &gt; 4 | *"Queue Violence: 300 Injured in Post Office Brawl"* | **-5 Order** |
| `GENEROUS` | &gt; 5 | *"Surprise Umbrella Donations Cause Rain Gear Market Crash"* | **+5 Prosperity** |

* * *

## 5. Promotions & Achievements (Gamification)

To simulate the feeling of "useless corporate middle-management," the game splits progression into two distinct tracks: **Achievements** (cosmetic collectibles) and **Promotions** (career advancement & difficulty).

### Achievements (Cosmetic Collectibles)

Achievements are standalone rewards earned by playing the game in specific ways. They function as collectibles—useless objects or certificates that clutter your workspace. They do **not** affect gameplay stats; they are just "passive-aggressive praise."

**The Shelf of Junk & Medals** As you complete specific hidden criteria, items appear on your desk or certificates are pinned to the wall. These celebrate both your successes and your failures.

- **"The Golden Stapler":** Process 100 Souls without error.
- **"Pyromaniac":** Kept the planet on fire for several days in a row (Example).
- **"Cactus":** You kept the Population stable (it's the only living thing allowed in the office).
- **"Most Magnificent Fuckup":** You caused a catastrophic event that somehow didn't result in immediate firing.

### Promotions (Career Progression)

Promotions are awarded for surviving. **Title changes coincide with increases in game difficulty.** As your rank increases, the game rules shift to become more demanding, though the specifics of these rule changes are TBD.

**Title Changes** Your rank on the daily dossier changes as you advance, becoming increasingly grand but ultimately meaningless.

- **Day 1:** Unpaid Temp.
- **Day 5:** Junior Pencil Guard.
- **Day 10:** Senior Scapegoat.
- **Day 15:** Assistant Manager.

* * *

## 6. End of Day Sequence

How the player experiences this page in the game loop (assuming **Option B** is chosen).

1. **Shift Ends.** The pneumatic tubes shut down.
2. **Transition.** The view fades from the Desk.
3. **Planet View.** We cut to a wide shot of the Planet (or a view from the office window looking out).

    - *Animation:* We see the changes happen. A skyscraper grows (Prosperity) or a fire breaks out (Chaos).
    - *Metrics Update:* Bars fill/drain with a mechanical sound.
4. **News Spin.** The Newspaper spins onto the screen over the city view.

    - *Headlines:* Read the consequences.
5. **Promotion Check.** If an achievement was met, God’s hand drops a "Golden Stapler" onto the screen with a confetti pop.

    - *God V.O.:* "Here. For your effort. Don't scratch it."
