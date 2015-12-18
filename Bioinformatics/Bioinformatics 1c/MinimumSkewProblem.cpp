//
// Created by weenkus on 12/17/15.
//
#include <iostream>
#include <fstream>
#include <vector>
#include <string>

int main() {
    // Open file handles
    std::ifstream inputHandle("input.txt");
    std::ofstream outputHandle("output.txt");
    std::string genome;
    std::getline(inputHandle, genome);

    // Clean up
    inputHandle.close();
    outputHandle.close();
    return 0;
}

std::vector<int> minSkewLocation(const std::string& genome) {
    int genLen = genome.length(), count{0};
    std::vector<int> countVector;

    for(const auto& c : genome) {
        if(c == 'C')
            --count;
        if(c == 'G')
            ++count;
        countVector.push_back(count);
    }

    bool falling = true, changed = false;
    if(countVector[0] >= 0)
        falling = false;
    for(int i{0}; i < countVector.size(); ++i) {
        if( i > 0 && countVector[i-1] < countVector[i] ) {
            if( falling == true)
                changed = true;
            falling = false;
        } else {
            if( falling == false)
                changed = true;
            falling = true;
        }

        if(changed) {
            countVector.push_back(i);
            changed = false;
        }
    }

}


bool fall(int previous, int current, bool wasRising) {
    bool rising = false;
    if( previous < current) {
        rising = true;
    }
}

