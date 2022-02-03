with open('input/day21.txt') as f:
    pos_1: int = int(f.readline().strip().split(': ')[1])
    pos_2: int = int(f.readline().strip().split(': ')[1])

# Part One
b: list[int] = [i for i in range(1, 11)]
die: list = [i for i in range(1, 101)] * 10
score_1: int = 0
score_2: int = 0
i: int = 0
while score_1 < 1000 and score_2 < 1000:
    if i % 2 == 0:
        pos_1 += sum(die[i:i+3])
        mod = pos_1 % 10
        if mod == 0:
            score_1 += 10
        else:
            score_1 += mod
    else:
        pos_2 += sum(die[i:i+3])
        mod = pos_2 % 10
        if mod == 0:
            score_2 += 10
        else:
            score_2 += mod
    i += 3
res = sorted([i, score_1, score_2])
print(res[0] * res[1])
