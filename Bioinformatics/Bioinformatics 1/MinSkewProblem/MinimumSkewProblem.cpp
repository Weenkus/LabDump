//
// Created by weenkus on 12/17/15.
//
#include <iostream>
#include <fstream>
#include <vector>
#include <string>

std::vector<int> findMinimumInAfunction(const std::vector<int> function);
std::vector<int> calculateSkew(const std::string& genome);

int main() {
    // Open file handles
    std::ifstream inputHandle("input.txt");
    std::ofstream outputHandle("output.txt");
    std::string genome;
    std::getline(inputHandle, genome);

	std::vector<int> skew = calculateSkew(genome);
	std::vector<int> minSkew = findMinimumInAfunction(skew);

	for (const auto& n : minSkew)
		outputHandle << n << ' ';

    // Clean up
    inputHandle.close();
    outputHandle.close();
    return 0;
}

std::vector<int> findMinimumInAfunction(const std::vector<int> function) {
	std::vector<int> mins;
	for (const auto& n : function) {
		mins.push_back(n);
	}
	return mins;
}

std::vector<int> calculateSkew(const std::string& genome) {
	int genLen = genome.length(), count{ 0 };
	std::vector<int> skew;

	skew.push_back(0);
	for (const auto& c : genome) {
		if (c == 'G')
			++count;
		else if (c == 'C')
			--count;
		skew.push_back(count);
	}
	return skew;
}
