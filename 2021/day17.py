s: str
xr: list[int] = []
yr: list[int] = []
with open('input/day17.txt') as f:
    s = f.read().strip().strip('target area: ').split(', ')
    xr = [int(k) for k in s[0].strip('x=').split('..')]
    yr = [int(k) for k in s[1].strip('y=').split('..')]

def ontarget(x: int, y: int, xr: list[int], yr: list[int]) -> bool:
    return x >= xr[0] and x <= xr[1] and y >= yr[0] and y <= yr[1]

def fx(x: int, vx: int) -> tuple[int, int]:
    x += vx
    if vx > 0:
        vx -= 1
    return x, vx

def fy(y: int, vy: int) -> tuple[int, int]:
    y += vy
    vy -= 1
    return y, vy

mxY: int = 0
cnt: int = 0
for xi in range(1, 500):
    for yi in range(-500, 500):
        x: int = 0
        vx: int = xi
        y: int = 0
        vy: int = yi
        mxy: int = 0
        while x <= xr[1] and y >= yr[0]:
            x, vx = fx(x, vx)
            y, vy = fy(y, vy)
            mxy = max(mxy, y)
            if ontarget(x, y, xr, yr):
                mxY = max(mxY, mxy)
                cnt += 1
                break

# Part One and Two
print(mxY, cnt)
