import utils
a = utils.get_input(day=2, data_type=str, sep='\n', preview_lines=6)

# Part One
forward = 0
depth   = 0
for command in a:
    d, x = command.split(' ')
    if d == 'forward':
        forward += int(x)
    elif d == 'down':
        depth += int(x)
    elif d == 'up':
        depth -= int(x)
print(forward*depth)
    
# Part Two 
forward = 0
depth   = 0
aim     = 0
for command in a:
    d, x = command.split(' ')
    if d == 'forward':
        forward += int(x)
        depth += aim * int(x)
    elif d == 'down':
        aim += int(x)
    elif d == 'up':
        aim -= int(x)
print(forward*depth)
