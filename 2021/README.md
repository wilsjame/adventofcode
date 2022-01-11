Using Python 3.10.1 and mypy. 

mypy typing hints follow the Python 3.9+ style so earlier versions of Python won't work. 

```
# Python 3.8 and earlier

from typing import List
x: List[int] = [4, 2]
```

```
# Python 3.9+

# built-in and generic types are lowercase and no need to import from typing
x: list[int] = [4, 2]
```
