# Thread Helper
This simple pattern allow me to do the following

* Allow developers to debug multi-threaded processed a thread at a time by setting the ThreadingOption to Single
* Throttles the multi-treaded process preventing the process from spinning too many threads that can consume too many resources
  * I am not showing it but I use this specifically to throttle various process during the business day

So leveraging what I found on MSDN here  https://msdn.microsoft.com/library/system.threading.tasks.taskscheduler.aspx. I 
