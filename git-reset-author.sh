#!/bin/sh

# Credits: http://stackoverflow.com/a/750191

git filter-branch -f --env-filter "
    GIT_AUTHOR_NAME='zerolanmando'
    GIT_AUTHOR_EMAIL='yiwei.shan@student.uts.edu.au'
    GIT_COMMITTER_NAME='zerolanmando'
    GIT_COMMITTER_EMAIL='yiwei.shan@student.uts.edu.au'
  " HEAD