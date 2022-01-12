data: list[str] = []
with open('input/day10.txt') as f:
    for line in f:
        data.append(line.strip())

# Part One
def match(l: str, r: str) -> bool:
    if l == '(' and r == ')' or l == '[' and r == ']' or l == '{' and r == '}' or l == '<' and r == '>':
        return True
    else:
        return False

ans:   int = 0
stack: list[str] = []
score: dict[str, int] = {')': 3, ']': 57, '}': 1197, '>': 25137}
incomplete: list[str] = []
for line in data:
    part2 = True
    for x in line:
        if x == ')' or x == ']' or x == '}' or x == '>':
            top = stack.pop(0)
            if match(top, x) == True:
                continue
            else:
                ans += score[x]
                part2 = False
                break
        else:
            stack.insert(0, x)
    if part2 == True:
        incomplete.append(line)
print(ans)

# Part Two
def pair(l: str) -> str:
    ret: str
    if l == '(':
        ret = ')' 
    elif l == '[':
        ret = ']'
    elif l == '{':
        ret = '}'
    elif l == '<':
        ret = '>'
    return ret

def calc_score(a: list[str], score: dict[str, int]) -> int:
    res = 0
    for x in a:
        res *= 5
        res += score[pair(x)]
    return res

points: list[int] = []
score = {')': 1, ']': 2, '}': 3, '>': 4}
for line in incomplete:
    stack = []
    for x in line:
        if x == ')' or x == ']' or x == '}' or x == '>':
            top = stack.pop(0)
            if match(top, x) == True:
                continue
            else:
                ans += score[x]
                part2 = False
                break
        else:
            stack.insert(0, x)
    points.append(calc_score(stack, score))
    points.sort()
print(points[len(points)//2])
