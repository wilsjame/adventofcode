data: list[str] = []
with open('input/day10.txt') as f:
    for line in f:
        data.append(line)

# Part One
def match(l: str, r: str) -> bool:
    if l == '(' and r == ')' or l == '[' and r == ']' or l == '{' and r == '}' or l == '<' and r == '>':
        return True
    else:
        return False

ans:   int = 0
stack: list[str] = []
score: dict[str, int] = {')': 3, ']': 57, '}': 1197, '>': 25137}
for line in data:
    for x in line:
        if x == ')' or x == ']' or x == '}' or x == '>':
            top = stack.pop(0)
            if match(top, x) == True:
                continue
            else:
                #print('invalid', x)
                ans += score[x]
                break
        else:
            stack.insert(0, x)
print(ans)
