SOURCES += main.cpp

# qmake defaults to adding -O2 when compiling, which can cause gcov to miss that some lines were optimised out.
QMAKE_CXXFLAGS_RELEASE -= -O2

# Compile with a high warning level, a warning is an error
QMAKE_CXXFLAGS += -Wall -Wextra -Weffc++ -Werror

# gcov
QMAKE_CXXFLAGS += -fprofile-arcs -ftest-coverage
LIBS += -lgcov
