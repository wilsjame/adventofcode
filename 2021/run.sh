#!/bin/bash
echo -e "WHAT DAY IS IT?"
read n
echo -e "RUNNING DAY $n"
while [ true ]
do
  python3 day$n.py
  sleep 0.25
done
