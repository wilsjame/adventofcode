def mapt(data_type=str, *args) -> tuple:
    """Map data_type to *args and return result as a tuple."""
    return tuple(map(data_type, *args))

def get_input(day=int, data_type=str, sep=str, preview_lines=7) -> tuple:
    """Split the day's input file into entries seperated by sep and, and apply data_type to each."""
    with open(f'input/day{day}.txt') as f:
        data = f.read()
    # parse data
    data = mapt(data_type, data.rstrip().split(sep))
    # preview data
    print(f'Day {day}\'s first {preview_lines} lines of input:') 
    for i in range(preview_lines):
        print(data[i], type(data[i]))
    print(f'... [{len(data) - preview_lines} entries not shown]')

    return data

def main():
    get_input(1, int, '\n')

main()
