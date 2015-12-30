#include <vector>
#include <string>
#include <fstream>
#include <iostream>
#include <set>

std::string numberToPattern(long unsigned int number, int k);
long unsigned int patternToNumberNonRecursive(std::string pattern);
long unsigned int patternToNumber(std::string pattern);

int symbolToNumber(char symbol);
int numberToSymbol(int number);

long unsigned int quotient(long unsigned int number, int divider);
int remainder(long unsigned int number, int divider);

std::vector<int> computingFreq(std::string text, int k, int d);

int main() {

	// Open files for input/output
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");

	std::string input;
	int number{ 0 }, k{ 0 }, int d{ 0 };
	inputHandle >> input >> k >> d;

	//outputHandle << patternToNumberNonRecursive(input);
	//outputHandle << numberToPattern(number, k);
	std::vector<int> freqArray = computingFreq(input, k, d);
	for (const auto& n : freqArray)
		outputHandle << n << " ";

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

int numberToSymbol(int number) {
	switch (number) {
	case 0:
		return 'A';
	case 1:
		return 'C';
	case 2:
		return 'G';
	case 3:
		return 'T';
	}
}

long unsigned int patternToNumberNonRecursive(std::string pattern) {
	int patLen = pattern.length();
	long unsigned int count{ 0 };
	for (int i{ 0 }; i < patLen; i++) {
		count += symbolToNumber(pattern.at(i)) * pow(4, patLen - i - 1);
	}
	return count;
}

std::string numberToPattern(long unsigned int number, int k) {
	std::vector<char> pattern;
	if (k == 1) {
		pattern.push_back(numberToSymbol(number));
	}
	else {
		for (int i{ 0 }; i < k; i++) {
			pattern.insert(pattern.begin(), numberToSymbol(remainder(number, 4)));
			number = quotient(number, 4);
		}
	}
	std::string str(pattern.begin(), pattern.end());
	return str;
}

long unsigned int quotient(long unsigned int number, int divider) {
	return number / divider;
}

int remainder(long unsigned int number, int divider) {
	return number % divider;
}

std::vector<int> computingFreq(std::string text, int k, int d) {
	// Initialise the freq array
	std::vector<int> freqs;
	int freqLen = pow(4, k) - 1, textLen = text.length();
	freqs.reserve(freqLen);
	for (int i{ 0 }; i <= freqLen; i++) {
		freqs.push_back(0);
	}

	// Compute the freq array
	std::string pattern;
	int j{ 0 };
	for (int i{ 0 }; i <= textLen - k; i++) {
		pattern = text.substr(i, k);
		j = patternToNumberNonRecursive(pattern);
		freqs[j] = freqs[j] + 1;
	}

	return freqs;
}
