a: dict[int, int] = {x: 0 for x in range(9)}
with open('input/day6.txt') as f:
    for k in [int(x) for x in f.read().strip().split(',')]:
        a[k] += 1

def update(a: dict[int, int]) -> dict[int, int]:
    b: dict[int, int] = {x: 0 for x in range(9)}
    for k, v in a.items():
        if k == 0:
            b[6] += v
            b[8] += v
        else:
            b[k - 1] += v
    return b

# Part One and Two
for _ in range(256):
    a = update(a)
print(sum(a.values()))
