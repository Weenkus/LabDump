#include <vector>
#include <string>
#include <iostream>
#include <fstream>

void patternMatch(std::string genome, std::string pattern);

int main() {

	// Open the file
	std::ifstream inputHandle("Vibrio_cholerae.txt");
	std::string genome, pattern;
	std::getline(inputHandle, pattern);
	std::getline(inputHandle, genome);

	patternMatch(genome, pattern);

	inputHandle.close();
	return 0;
}

void patternMatch(std::string genome, std::string pattern) {
	int genLen = genome.length(), patLen = pattern.length(), count{ 0 };
	std::ofstream outputHandel("output.txt");
	for (int i{ 0 }; i < genLen - patLen; i++) {
		if (genome.substr(i, patLen).compare(pattern) == 0)
			outputHandel << i << " ";
	}
	outputHandel.close();
}