#!/usr/bin/python3
import sys
import subprocess
import os
import stat
import signal
import time

def handle(sig_received, frame):
    print('SIGINT or CTRL-C detected, exiting!!!')
    # chmod -x
    os.chmod(script, os.stat(script).st_mode & ~stat.S_IXUSR)
    exit(0)

signal.signal(signal.SIGINT, handle)
n = sys.argv[1]
if n.isdigit():
    script = f"day{n}.py" # lol dangerous
    # chmod +x 
    os.chmod(script, os.stat(script).st_mode | stat.S_IXUSR)
    # execute
    while True:
        subprocess.run(["python3", script]) 
        time.sleep(0.25)
else:
    print("INPUT THE DAY NUMBER")
