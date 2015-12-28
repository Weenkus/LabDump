#include <fstream>
#include <iostream>
#include <string>
#include <vector>

int patternCount(std::string genome, std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {

	// Open and read the file
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");
	std::string genome, pattern;
	int d{ 0 };

	// Parse
	inputHandle >> pattern >> genome >> d;

	outputHandle << patternCount(genome, pattern, d);

	// Clean afteryouself
	inputHandle.close();
	outputHandle.close();
	return 0;
}

int patternCount(std::string genome, std::string pattern, int d) {
	int genLen = genome.length(), patLen = pattern.length(), count{ 0 };
	for (int i{ 0 }; i < genLen - patLen + 1; i++) {
		if (countMissmatch(genome.substr(i, patLen), pattern) <= d) {
			++count;
		}
	}
	return count;
}

int countMissmatch(const std::string& genome1, const std::string& genome2) {
	int count{ 0 };
	for (int i{ 0 }; i < genome1.size(); ++i)
		count += genome1.at(i) != genome2.at(i) ? 1 : 0;
	return count;
}