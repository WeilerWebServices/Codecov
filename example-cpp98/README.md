# [Codecov][1] qmake_gcc_cpp98_gcov Example
[![Travis CI logo](TravisCI.png)](https://travis-ci.org)
![Whitespace](Whitespace.png)
[![Codecov logo](Codecov.png)](https://www.codecov.io)

[![Build Status](https://travis-ci.org/richelbilderbeek/travis_qmake_gcc_cpp98_gcov.svg?branch=master)](https://travis-ci.org/richelbilderbeek/travis_qmake_gcc_cpp98_gcov)
[![codecov.io](https://codecov.io/github/richelbilderbeek/travis_qmake_gcc_cpp98_gcov/coverage.svg?branch=master)](https://codecov.io/github/richelbilderbeek/travis_qmake_gcc_cpp98_gcov?branch=master)

The goal of this project is to have a clean Travis CI build, with specs:
 * C++ version: `C++98`
 * Build system: `qmake`
 * C++ compiler: `g++`
 * Libraries: `STL` only
 * Code coverage: `gcov` (note: it should show the code coverage is below 100%)
 * Source: multiple files

Additionally, the code coverage should be measured by CodeCov.
## Guide
### Travis Setup

Add to your `.travis.yml` file.
```yml
language: cpp
compiler: gcc

before_install: 
 - sudo pip install codecov

script: 
 - ./build.sh
 - ./travis_qmake_gcc_cpp98_gcov
 - ./get_code_cov.sh
 - codecov
```
### Produce Coverage Reports
#### gvoc
```sh
#!/bin/bash
for filename in `find . | egrep '\.cpp'`; 
do 
  gcov -n -o . $filename > /dev/null; 
done
```
## Caveats
### Private Repos
Repository tokens are required for (a) all private repos, (b) public repos not using Travis-CI, CircleCI or AppVeyor. Find your repository token at Codecov and provide via `codecov --token=:token` or `export CODECOV_TOKEN=":token"`
## More complex builds
 * C++11: [travis_qmake_gcc_cpp11_gcov](https://www.github.com/richelbilderbeek/travis_qmake_gcc_cpp11_gcov)
 * C++14: [travis_qmake_gcc_cpp14_gcov](https://www.github.com/richelbilderbeek/travis_qmake_gcc_cpp14_gcov)

1. More documentation at https://docs.codecov.io
2. Configure codecov through the `codecov.yml`  https://docs.codecov.io/docs/codecov-yaml

We are happy to help if you have any questions. Please contact email our Support at [support@codecov.io](mailto:support@codecov.io)

[1]: https://codecov.io/
