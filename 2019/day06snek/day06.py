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

# part 2
def dfs2(s: str, path: list[str]) -> list[str]:
    "returns the path from to s to COM"
    if s == 'COM':
        return path
    for u in adj[s]:
        path.append(u)
        dfs2(u, path)
    return path

path1 = (dfs2('YOU', ['YOU']))
path2 = (dfs2('SAN', ['SAN']))

# where the paths meet
meet: str = None
for x in path1: 
    if path2.count(x) > 0:
        meet = x
        break

"""
print(meet)
print(path1.index(meet), path1)
print(path2.index(meet), path2)
"""

d1 = path1.index(meet)
d2 = path2.index(meet)
total = d1 + d2 - 1 # minus 1 because meet is counted twice
print(total-1)      # minus 1 to count links instead of steps

