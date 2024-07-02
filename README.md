# What is it?

A quick and dirty Star Citizen total playtime calculator based on your Game logs (*.log files in SC folder).  
You need to give the path to your logbackups folder and it'll do the rest.

# How do I run this?

## Executable
If you trust me, you can grab the exe in release and open that, then select the folder "logbackups" in your StarCitizen install. 
You don't have to trust me if you can run the python code yourself (and even generate the executable) from the main branch.  
Otherwise clone the repo, build the solution (C#) yourself, or run the Python code yourself :)

## What the program does

SCPlay Iterates over logs, identifies dates and timestamps using regexps, calculates delta per-log, adds them all up. That's it.

Since the stats aren't available anywhere, the only way to gather that information is by going through the log files (current session `Game.log` and past sessions in the `logbackup/*.log` files)  

The logic is basic and has very little error handling.  
Looks for log files in logbackups, adding the `Game.log` file one folder up (most recent log), and then calculates deltas within each of those files between the first timestamp and the last one.  

# License

Do whatever you want with it, it's not mine it's everybody's. Cheers.  
(Tentatively put MIT but probably more [WTFPL](http://en.wikipedia.org/wiki/WTFPL) to be honest!)