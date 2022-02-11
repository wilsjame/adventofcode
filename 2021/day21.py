class Dice():
    def __init__(self):
        self.cnt: int = 0
        self.roll_cnt: int = 0

    def roll(self) -> int:
        inc: int = 1
        self.cnt = (self.cnt + inc - 1) % 10 + 1
        self.roll_cnt += 1
        return self.cnt

die = Dice()

class Player():
    def __init__(self, start):
        self.pos: int = start
        self.score: int = 0

    def move(self, spaces: int) -> int:
        self.pos = (self.pos + spaces - 1) % 10 + 1 
        return self.pos

players: list[Player] = [Player(8), Player(10)]

# Part One
turn: int = 0
while True:
    who: int = turn % 2
    players[who].score += players[who].move(die.roll() + die.roll() + die.roll())
    turn += 1
    if players[who].score > 999: 
        break
print(min(players[0].score, players[1].score) * die.roll_cnt)

"""
def solve(turn: int=0) -> None:
    who: int = turn % 2
    players[who].score += players[who].move(die.roll() + die.roll() + die.roll())
    if players[0].score > 999 or players[1].score > 999:
        return
    solve(turn+1)
solve()
print(min(players[0].score, players[1].score) * die.roll_cnt)
"""

# Part Two
# brute force with memo 10 spaces * 10 spaces * 21 pts * 21 pts = 44100 max
Score = int
Pos   = int
GameState = tuple[Score, Pos, Score, Pos]
memo: dict[tuple[GameState], tuple[int, int]] = {}

# player 1 is initially passed to the p1 parameters
# since the players take turns player 1 is then passed to the
# p2 parameter to wait for player 2 to play next as p1. 
def solve(p1_score=0, p1_pos=8, p2_score=0, p2_pos=10) -> tuple[int, int]:
    if p1_score >= 21:
        return (1, 0)
    if p2_score >= 21:
        return (0, 1)

    state: GameState = (p1_score, p1_pos, p2_score, p2_pos)
    if state in memo:
        return memo[state]

    p1_wins = 0
    p2_wins = 0
    for d1 in (1, 2, 3):
        for d2 in (1, 2, 3):
            for d3 in (1, 2, 3):
                sumdie = d1 + d2 + d3
                r2, r1 = solve(p2_score, p2_pos, p1_score+((p1_pos+sumdie-1)%10+1), (p1_pos+sumdie-1)%10+1)
                p1_wins += r1
                p2_wins += r2
    memo[state] = (p1_wins, p2_wins)
    return memo[state]
print(max(solve()))

"""
from collections import defaultdict
from typing import DefaultDict
Score    = int
Position = int
State    = tuple[Position, Score, Position, Score]

cur_universes: DefaultDict[State, int] = defaultdict(int)
start_1: Position = 8
start_2: Position = 10
cur_universes[(start_1, 0, start_2, 0)] = 1
p1_wins: int = 0
p2_wins: int = 0

die: list[int] = []
for d1 in [1, 2, 3]:
    for d2 in [1, 2, 3]:
        for d3 in [1, 2, 3]:
            die.append(d1 + d2 + d3)

while cur_universes:
    nxt_universes: DefaultDict[State, int] = defaultdict(int)
    for state, cnt in cur_universes.items():
        pos_1, scr_1, pos_2, scr_2 = state
        # play game 
        # player 1
        nupos_1: Position
        nuscr_1: Score
        for total_roll in die:
            nupos_1 = (pos_1 + total_roll - 1) % 10 + 1
            nuscr_1 = scr_1 + nupos_1
            if nuscr_1 >= 21:
                p1_wins += cnt
                continue
            # player 2
            nupos_2: Position
            nuscr_2: Score
            for total_roll in die:
                nupos_2 = (pos_2 + total_roll - 1) % 10 + 1
                nuscr_2 = scr_2 + nupos_2
                if nuscr_2 >= 21:
                    p2_wins += cnt
                    continue
                # scores tend to >= 21, eventually nxt_universes will be empty
                nxt_universes[(nupos_1, nuscr_1, nupos_2, nuscr_2)] += cnt
    cur_universes = nxt_universes
print(p1_wins, p2_wins)
"""
