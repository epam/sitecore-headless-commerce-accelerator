wmic Path win32_process Where "CommandLine Like '%%selenium-server%%'" Call Terminate
wmic Path win32_process Where "CommandLine Like '%%geckodriver%%'" Call Terminate
wmic Path win32_process Where "CommandLine Like '%%chromedriver%%'" Call Terminate
exit 0