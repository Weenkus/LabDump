#include <vector>
#include <string>
#include <fstream>
#include <iostream>

long unsigned int patternToNumber(std::string pattern);
int symbolToNumber(char symbol);

int main() {

	// Open files for input/output
	std::ifstream inputHandle("dataset_3010_2.txt");
	std::ofstream outputHandle("output.txt");

	std::string input;
	std::getline(inputHandle, input);

	outputHandle << patternToNumber(input);

	inputHandle.close();
	outputHandle.close();
	return 0;
}

long unsigned int patternToNumber(std::string pattern) {
	int patLen = pattern.length();
	if (patLen == 0)
		return 0;
	return (4 * patternToNumber(pattern.substr(0, patLen - 1))) + symbolToNumber(pattern[patLen - 1]);
}

int symbolToNumber(char symbol) {
	switch (symbol) {
	case 'A':
		return 0;
	case 'C':
		return 1;
	case 'G':
		return 2;
	case 'T':
		return 3;
	}
}