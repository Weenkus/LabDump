cmake_minimum_required(VERSION 3.3)
project(Bioinformatics_1c)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++14")

set(SOURCE_FILES MinimumSkewProblem.cpp)
add_executable(MinSkew ${SOURCE_FILES})