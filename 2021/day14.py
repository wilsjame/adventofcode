from collections import Counter

a: list[str] = []
d: dict[str, str] = {}
cnt: Counter[str] = Counter()
with open('input/day14.txt') as f:
    for line in f:
        if line == '\n':
            continue
        elif '->' not in line:
            a = list(line.strip())
        else:
            k, v = line.strip().split(' -> ')
            d[k] = v

# nearing al dente 
def update(cnt: Counter[str], d: dict[str, str], c: Counter[str]) -> Counter[str]:
    """cnt counts pairs and c counts single characters"""
    cnt2: Counter[str] = Counter()
    for k, v in cnt.items():
        if v > 0:
            l,r = str(k[0]+d[k]), str(d[k]+k[1])
            c[d[k]] += v
            cnt2[l] += v
            cnt2[r] += v
    return cnt2

# seed initial pairs
for i in range(len(a) -1):
    pr = str(a[i]+a[i+1])
    cnt[pr] += 1

# Part One and Two
c: Counter[str] = Counter(a)
steps: int = 40
for k in range(steps):
    cnt = update(cnt, d, c)
print(c.most_common()[0][1] - c.most_common()[-1][1])
