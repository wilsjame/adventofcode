class Dice():
    def __init__(self):
        self.cnt: int = 0
        self.roll_cnt: int = 0

    def roll(self) -> int:
        self.cnt += 1
        self.cnt %= 10
        if self.cnt == 0:
            self.cnt = 10
        self.roll_cnt += 1
        return self.cnt

die = Dice()

class Player():
    def __init__(self, start):
        self.pos: int = start
        self.score: int = 0

    def move(self, spaces: int) -> int:
        self.pos += spaces
        self.pos %= 10
        if self.pos == 0:
            self.pos = 10
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
