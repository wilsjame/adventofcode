from collections import defaultdict
graph = defaultdict[str, list[str]]

adj: graph = defaultdict(list)
with open('input/day12.txt') as f:
    for line in f:
        u,v = line.strip().split('-')
        adj[u].append(v)
        adj[v].append(u)

# Part One (allowed to visit lower case nodes once)
def dfs(path: list[str], adj: graph, allpaths: list[list[str]]) -> None:
    """brute force all paths from start to end nodes"""
    if path[-1] == 'end':
        allpaths.append(path)
    else:
        for u in adj[path[-1]]:
            if u.isupper() or u not in path:
                dfs(path + [u], adj, allpaths)

path: list[str] = ['start']
allpaths: list[list[str]] = []
dfs(path, adj, allpaths)
print(len(allpaths))

# Part Two (allowed to visit one lowercase node twice)
def visited_twice(path: list[str]) -> bool:
    """return true if a lower case node has been visited twice in the path"""
    for u in path:
        if u.islower() and path.count(u) > 1:
            return True
    return False

def dfs_2(path: list[str], adj: graph, allpaths: list[list[str]]) -> None:
    """brute force all paths from start to end nodes"""
    if path[-1] == 'end':
        allpaths.append(path)
    else:
        for u in adj[path[-1]]:
            if u == 'start':
                pass
            elif u.isupper() or u not in path or visited_twice(path) == False:
                dfs_2(path + [u], adj, allpaths)

path = ['start']
allpaths = []
dfs_2(path, adj, allpaths)
print(len(allpaths))
