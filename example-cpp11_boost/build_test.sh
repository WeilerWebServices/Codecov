#!/bin/bash
./clean.sh
qmake travis_qmake_gcc_cpp11_boost_test_gcov_test.pro
make
./travis_qmake_gcc_cpp11_boost_test_gcov_test
gcov-5 main_test.cpp
gcov-5 my_functions.cpp

# Create gcov files
#for filename in `ls *.cpp`; do gcov $filename; done
#for filename in `ls *.h`; do gcov $filename; done

# Display gcov files
#for filename in `ls *.h.gcov`; do cat $filename; done
#for filename in `ls *.cpp.gcov`; do cat $filename; done

