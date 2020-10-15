#!/bin/bash
qmake travis_qmake_gcc_cpp11_boost_test_gcov.pro
make debug
./travis_qmake_gcc_cpp11_boost_test_gcov