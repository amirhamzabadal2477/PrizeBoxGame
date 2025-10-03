# PrizeBoxGame - Task #4 Submission

## Overview

PrizeBoxGame is a console application that simulates the Rick and Morty prize box game with an arbitrary number of boxes.

## Features

- Configurable number of boxes (N > 2) via command-line arguments
- Optional Morty class to define strategy
- Provably fair HMAC-based randomness
- Simulation-based probability estimation
- User-friendly console output

## Usage

From the project folder (You need to be in the "PrizeBoxGame" folder), run:
```bash
dotnet run -- <number_of_boxes> [MortyClass]

******************
## Examples
On_terminal (input):

dotnet run -- 5
dotnet run -- 3 EvilMorty
dotnet run -- 10 SmartMorty

Output1:
🎲 Starting Prize Box Game with 5 boxes...
🤖 Using Morty: DefaultMorty

📊 Simulation Results (10000 rounds):
   Stay strategy win rate:   20.12%
   Switch strategy win rate: 79.88%

Output2:
🎲 Starting Prize Box Game with 3 boxes...
🤖 Using Morty: EvilMorty

📊 Simulation Results (10000 rounds):
   Stay strategy win rate:   33.21%
   Switch strategy win rate: 66.79%

Output3:
🎲 Starting Prize Box Game with 10 boxes...
🤖 Using Morty: SmartMorty

📊 Simulation Results (10000 rounds):
   Stay strategy win rate:   10.05%
   Switch strategy win rate: 89.95%
