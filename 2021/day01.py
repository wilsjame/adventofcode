import utils
a = utils.get_input(day=1, data_type=int, sep='\n')

# Part One
cnt = 0
for i in range(len(a) - 1):
    cnt += a[i+1] > a[i]
print(cnt)

# Part Two 
def window(i: int, a: tuple) -> int:
    """returns the sum of the three consecutive values beginning at i"""
    res = 0
    for j in range(i, min(i + 3, len(a))):
        res += a[j]
    return res

cnt = 0
for i in range(1, len(a) - 2):
    cnt += window(i, a) > window(i - 1, a)
print(cnt)
