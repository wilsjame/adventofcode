""" 
Part One: done by hand

#############
#...........# <- hallway
###D#A#A#D### <- room
  #C#C#B#B# <--- room
  #########

Part Two

this type of graph search problem is like Magic Squares
from USACO training 3.2 which was from IOI'96

we brute force board arrangements and memoize the mininum cost 
to reach each arrangement. we call the recursive search with 
the start state and make our base case the finish state. the 
final return value is the minimal cost to reach the finish. 

alt approach: bfs, bfs+heap, A*, ... where each arrangement is
              is a node and edges are transformations

#############
#...........#
###D#A#A#D###
  #D#C#B#A#
  #D#B#A#C#
  #C#C#B#B#
  #########
"""
from copy import deepcopy
State = dict[int, str]

# hard code input here
start: State = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
                        11: 'D',       12: 'A',        13: 'A',        14: 'D',
                        15: 'D',       16: 'C',        17: 'B',        18: 'A',
                        19: 'D',       20: 'B',        21: 'A',        22: 'C', 
                        23: 'C',       24: 'C',        25: 'B',        26: 'B'
        }
sample: State = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
                        11: 'B',       12: 'C',        13: 'B',        14: 'D',
                        15: 'D',       16: 'C',        17: 'B',        18: 'A',
                        19: 'D',       20: 'B',        21: 'A',        22: 'C', 
                        23: 'A',       24: 'D',        25: 'C',        26: 'A'
        }
 
test: State = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
                        11: 'A',       12: 'A',        13: 'C',        14: 'D',
                        15: 'A',       16: 'B',        17: 'C',        18: 'D',
                        19: 'A',       20: 'B',        21: 'C',        22: 'D', 
                        23: 'B',       24: 'B',        25: 'C',        26: 'D'
        }

finish: State = {
        0: '.', 1: '.', 2: '.', 3: '.', 4: '.', 5: '.', 6: '.', 7: '.', 8: '.', 9: '.', 10: '.',
                        11: 'A',       12: 'B',        13: 'C',        14: 'D',
                        15: 'A',       16: 'B',        17: 'C',        18: 'D',
                        19: 'A',       20: 'B',        21: 'C',        22: 'D', 
                        23: 'A',       24: 'B',        25: 'C',        26: 'D'
        }
cost: dict[str, int] = {'A': 1, 'B': 10, 'C': 100, 'D': 1000}
entry: dict[str, int] = {'A': 2,        'B': 4,         'C': 6,         'D': 8}
up: dict[int, int] = {23: 19, 19: 15, 15: 11, 11: 2, 24: 20, 20: 16, 16: 12, 12: 4, 25: 21, 21: 17, 17: 13, 13: 6, 26: 22, 22: 18, 18: 14, 14: 8}
down = {v: k for k, v in up.items()}
bottom: dict[str, int] = {'A': 23,      'B': 24,        'C': 25,        'D': 26}

def debug(b: State) -> None:
    print('#############')
    print(f'#{b[0]}{b[1]}{b[2]}{b[3]}{b[4]}{b[5]}{b[6]}{b[7]}{b[8]}{b[9]}{b[10]}#')
    print(f'###{b[11]}#{b[12]}#{b[13]}#{b[14]}###')
    print(f'  #{b[15]}#{b[16]}#{b[17]}#{b[18]}#')
    print(f'  #{b[19]}#{b[20]}#{b[21]}#{b[22]}#')
    print(f'  #{b[23]}#{b[24]}#{b[25]}#{b[26]}#')
    print('  #########\n')

# POD AT i IS IN A ROOM
def isFinal(b: State, i: int, pod: str) -> bool:
    """is pod at i in it's final position"""
    if finish[i] != pod:
        return False
    j: int = bottom[pod]
    while j >= i:
        if b[j] != finish[j]:
            return False
        j = up[j]
    return True

def canExit(b: State, i: int, pod: str) -> bool:
    """is pod at i able to exit it's room into the hallway"""
    while i >= 11:
        i = up[i]
        if b[i] != '.':
            return False
    return True

def leftHall(b: State, i: int) -> list[int]:
    """return valid left hall positions for pod at i"""
    valid: list[int] = []
    e: int = i
    while e in up:
        e = up[e] 
    for j in range(e-1, -1, -1):
        if j in down:
            continue
        if b[j] != '.':
            break
        valid.append(j)
    return valid

def rightHall(b: State, i: int) -> list[int]:
    """return valid right hall positions for pod at i"""
    valid: list[int] = []
    e: int = i
    while e in up:
        e = up[e]
    for j in range(e+1, 11):
        if j in down:
            continue
        if b[j] != '.':
            break
        valid.append(j)
    return valid

def move2hall(b: State, i: int, j: int, pod: str) -> (State, int):
    """moves pod at i to j and returns the new state and cost"""
    d: int = 0
    e: int = i
    while e in up:
        e = up[e]
        d += 1
    d += abs(e - j)
    b2 = deepcopy(b)
    b2[i] = '.'
    b2[j] = pod
    return b2, d*cost[pod]

# POD AT i IS IN THE HALL
def leftFinal(b: State, i: int, pod: str) -> int:
    """return valid left final position for pod at i or -1 if no valid move"""
    for j in range(i-1, -1, -1):
        if b[j] != '.':
            break
        if j == entry[pod]:
            if b[bottom[pod]] == '.':
                return bottom[pod]
            e: int = j
            while e in down:
                e = down[e]
                if b[e] == '.':
                    continue
                if isFinal(b, e, pod):
                    return up[e]
                else:
                    return -1
    return -1 

def rightFinal(b: State, i: int, pod: str) -> list[int]:
    """return valid right final positions for pod at i or -1 if no valid move"""
    valid: list[int] = []
    for j in range(i+1, 11):
        if b[j] != '.':
            break
        if j == entry[pod]:
            if b[bottom[pod]] == '.':
                return bottom[pod]
            e: int = j
            while e in down:
                e = down[e]
                if b[e] == '.':
                    continue
                if isFinal(b, e, pod):
                    return up[e]
                else:
                    return -1
    return -1

def move2room(b: State, i: int, j: int, pod: str) -> (State, int):
    """moves pod at i to j and returns the new state and cost"""
    d: int = abs(entry[pod] - i)
    e: int = entry[pod]
    while e < j:
        e = down[e]
        d += 1
    b2 = deepcopy(b)
    b2[i] = '.'
    b2[j] = pod
    return b2, d*cost[pod]

# SOLVE 
memo: dict[State, int] = {}
Hashed_State = tuple[tuple[int, str], ...]
def move(b: State) -> int:
    if b == finish:
        return 0
    b_key: Hashed_State = tuple(b.items())

    if b_key in memo:
        return memo[b_key]

    best_cost: int = 1e9 
    for i, pod in b.items():
        if pod == '.':
            continue
        if i >= 11:
            # in a room
            if canExit(b, i, pod) and isFinal(b, i, pod) == False:
                left_hall: list[int] = leftHall(b, i)
                for j in left_hall:
                    b2, c = move2hall(b, i, j, pod)
                    best_cost = min(best_cost, move(b2) + c)

                right_hall: list[int] = rightHall(b, i)
                for j in right_hall:
                    b2, c = move2hall(b, i, j, pod)
                    best_cost = min(best_cost, move(b2) + c)
        else:
            # in hallway
            j: int = leftFinal(b, i, pod)
            if j > -1:
                b2, c = move2room(b, i, j, pod)
                best_cost = min(best_cost, move(b2) + c)

            j = rightFinal(b, i, pod)
            if j > -1:
                b2, c = move2room(b, i, j, pod)
                best_cost = min(best_cost, move(b2) + c)

    # at this point we have the best cost for the parent call on the recursion stack
    # note: master parent is the start state, so the final return will be the best_cost to final from start 
    memo[b_key] = best_cost
    return best_cost

# our base case is reaching the finish state
# therefore, the return value for the parent state, start, 
# is the minimal cost, best_cost, to reach the base case (i.e. finish)
print(move(start))

