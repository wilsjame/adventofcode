a = []
with open('input/day7.txt') as f:
    a = [int(x) for x in f.read().strip().split(',')]

# Part One
ans = float("INF")
for x in range(max(a) + 1):
    cnt = 0
    for k in a:
        cnt += abs(x - k)
    ans = min(ans, cnt)
print(ans)

# Part Two
ans = float("INF")
for x in range(max(a) + 1):
    cnt = 0
    for k in a:
        d = abs(x - k)
        cnt += (d * (d + 1)) / 2
    ans = min(ans, cnt)
print(ans)
