from typing import cast
data: list[tuple[str, str]] = []

with open('input/day8.txt') as f:
    for line in f:
        data.append(cast(tuple[str, str], tuple(line.strip().split(' | '))))

# Part One
cnt = 0
for entry in data:
    a = entry[1].split()
    for k in a:
        # 1, 4, 7, 8
        n = len(k)
        if n == 2 or n == 4 or n == 3 or n == 7:
            cnt += 1
print(cnt)

# Part Two
table: dict[int, frozenset[str]]
sz5: list[frozenset[str]]
sz6: list[frozenset[str]]

cnt = 0
for entry in data:
    table = {}
    sz5   = []
    sz6   = []
    i     = entry[0].split()
    o     = entry[1].split()

    # solve unique segment digits 1, 4, 7, 8
    for x in i:
        n = len(x)
        s = frozenset(x)
        if n == 2:
            table[1] = s
        elif n == 3:
            table[7] = s
        elif n == 4:
            table[4] = s
        elif n == 7:
            table[8] = s
        elif n == 5:
            sz5.append(s)
        elif n == 6:
            sz6.append(s)

    # solve 5 segment digits 2, 3, 5
    # single out 3 with 2 union 5 = 8
    if sz5[0] | sz5[1] == table[8]:
        table[3] = sz5[2]
    elif sz5[0] | sz5[2] == table[8]:
        table[3] = sz5[1]
    elif sz5[1] | sz5[2] == table[8]:
        table[3] = sz5[0]
    # single out 5 with 4 intersect 5 = 3 in common (exclude found 3)
    if len(table[4] & sz5[0]) == 3 and sz5[0] not in table.values():
        table[5] = sz5[0]
    elif len(table[4] & sz5[1]) == 3 and sz5[1] not in table.values():
        table[5] = sz5[1]
    elif len(table[4] & sz5[2]) == 3 and sz5[2] not in table.values():
        table[5] = sz5[2]
    # single out 2 with 4 intersect 2 = 2 in common
    if len(table[4] & sz5[0]) == 2:
        table[2] = sz5[0]
    elif len(table[4] & sz5[1]) == 2:
        table[2] = sz5[1]
    elif len(table[4] & sz5[2]) == 2:
        table[2] = sz5[2]

    # solve 6 segment digits 0, 6, 9 nice
    # single out 6 with 1 intersect 6 = 1 in common
    if len(table[1] & sz6[0]) == 1:
        table[6] = sz6[0]
    elif len(table[1] & sz6[1]) == 1:
        table[6] = sz6[1]
    elif len(table[1] & sz6[2]) == 1:
        table[6] = sz6[2]
    # single out 9 with 3 intersect 9 = 5 in common
    if len(table[3] & sz6[0]) == 5:
        table[9] = sz6[0]
    elif len(table[3] & sz6[1]) == 5:
        table[9] = sz6[1]
    elif len(table[3] & sz6[2]) == 5:
        table[9] = sz6[2]
    # last unknown is 0
    for v in sz6:
        if v not in table.values():
            table[0] = v
    
    res = dict((v, k) for k, v in table.items())
    num = []
    for x in o:
        s = frozenset(x)
        num.append(str(res[s]))
    cnt += int(''.join(num))
print(cnt)
