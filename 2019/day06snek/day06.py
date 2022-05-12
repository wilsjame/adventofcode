from collections import defaultdict
graph = defaultdict[str, list[str]]

adj: graph = defaultdict(list)
myset: set[str] = set()

with open('day06.in') as f:
    for line in f:
        u,v = line.strip().split(')')
        adj[v].append(u)
        myset.add(u)
        myset.add(v)

def dfs(s: str, cnt: int) -> int:
    "returns the length of the path from s to COM"
    if s == 'COM':
        return cnt
    for u in adj[s]:
        cnt = dfs(u, cnt + 1)
    return cnt

# part 1
ans: int = 0
for v in myset:
    ans += dfs(v, 0)
print(ans)

