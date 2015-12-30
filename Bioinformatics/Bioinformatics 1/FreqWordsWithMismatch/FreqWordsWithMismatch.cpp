#include <vector>
#include <string>
#include <fstream>
#include <iostream>
#include <set>
#include <map>

std::string numberToPattern(long unsigned int number, int k);
long unsigned int patternToNumberNonRecursive(std::string pattern);
long unsigned int patternToNumber(std::string pattern);

long unsigned int quotient(long unsigned int number, int divider);
int remainder(long unsigned int number, int divider);

std::set<std::string> computingFreq(std::string text, int k, int d);

std::set<std::string> neigbours(std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);
int patternCount(std::string genome, std::string pattern, int d);

std::map<char, int> symbolToNumber = { {'A', 0} , {'C', 1}, {'G', 2}, {'T', 3} };
char numberToSymbol[4] = { 'A', 'C', 'G', 'T' };

std::vector<char> complementSequence(std::string strand);

int main() {

	// Open files for input/output
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");

	std::string input;
	int number{ 0 }, k{ 0 }, d{ 0 };
	inputHandle >> input >> k >> d;

	std::set<std::string> freqArray = computingFreq(input, k, d);
	for (auto n : freqArray)
		outputHandle << n << " ";

	inputHandle.close();
	outputHandle.close();
	return 0;
}

long unsigned int patternToNumber(std::string pattern) {
	int patLen = pattern.length();
	if (patLen == 0)
		return 0;
	return (4 * patternToNumber(pattern.substr(0, patLen - 1))) + symbolToNumber[pattern[patLen - 1]];
}

long unsigned int patternToNumberNonRecursive(std::string pattern) {
	int patLen = pattern.length();
	long unsigned int count{ 0 };
	for (int i{ 0 }; i < patLen; i++) {
		count += symbolToNumber[pattern.at(i)] * pow(4, patLen - i - 1);
	}
	return count;
}

std::string numberToPattern(long unsigned int number, int k) {
	std::vector<char> pattern;
	if (k == 1) {
		pattern.push_back(numberToSymbol[number]);
	}
	else {
		for (int i{ 0 }; i < k; i++) {
			pattern.insert(pattern.begin(), numberToSymbol[remainder(number, 4)]);
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

std::set<std::string> computingFreq(std::string text, int k, int d) {
	// Initialise the freq array
	std::vector<int> freqs, closed;
	int freqLen = pow(4, k) - 1, textLen = text.length();
	freqs.reserve(freqLen);
	for (int i{ 0 }; i <= freqLen; ++i) {
		freqs.push_back(0);
		closed.push_back(0);
	}

	// Generate neighbours
	for (int i{ 0 }; i < textLen - k; ++i) {
		std::set<std::string> neighs = neigbours(text.substr(i, k), d);
		for (auto n : neighs) 
			closed[patternToNumber(n)] = 1;
	}

	// Compute the freq array
	std::string pattern;
	for (int i{ 0 }; i < freqLen; ++i) {
		if (closed[i] == 1) {
			std::vector<char> compSeq = complementSequence(numberToPattern(i, k));
			std::string compSeqStr(compSeq.begin(), compSeq.end());
			freqs[i] = patternCount(text, numberToPattern(i, k), d) + patternCount(text, compSeqStr, d);
		}
	}

	// Find max in freq array
	int max{ 0 };
	for (int i{ 0 }; i < freqLen; ++i) {
		max = max > freqs[i] ? max : freqs[i];
	}

	// Calculate the freq pattern
	std::set<std::string> freqPattern;
	for (int i{ 0 }; i < freqLen; ++i) {
		if (freqs[i] == max) {
			freqPattern.insert(numberToPattern(i, k));
		}
	}

	return freqPattern;
}

std::set<std::string> neigbours(std::string pattern, int d) {
	std::set<std::string> fourNucleotides = { "A", "C", "G", "T" };
	std::set<std::string> tempPattern = { pattern };

	if (d == 0)
		return tempPattern;
	if (pattern.size() == 1)
		return fourNucleotides;

	std::set<std::string> negibourhood;
	std::string suffix = pattern.substr(1, pattern.size() - 1);
	std::set<std::string> suffixNegibourhood = neigbours(suffix, d);

	for (auto n : suffixNegibourhood) {
		if (countMissmatch(suffix, n) < d) {
			for (auto b : fourNucleotides) {
				negibourhood.insert(b.append(n));
			}
		}
		else {
			std::string concat = n;
			concat.insert(concat.begin(), pattern.at(0));
			negibourhood.insert(concat);
		}
	}
	return negibourhood;
}

int countMissmatch(const std::string& genome1, const std::string& genome2) {
	int count{ 0 }, genLen1 = genome1.size(), genLen2 = genome2.size();
	int lenMin = genLen1 > genLen2 ? genLen2 : genLen1;
	for (int i{ 0 }; i < lenMin; ++i)
		count += genome1.at(i) != genome2.at(i) ? 1 : 0;
	return count;
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

std::vector<char> complementSequence(std::string strand) {
	std::vector<char> complementStrand;
	for (const auto& c : strand) {
		char complement;
		switch (c) {
		case 'A':
			complement = 'T';
			break;
		case 'C':
			complement = 'G';
			break;
		case 'T':
			complement = 'A';
			break;
		case 'G':
			complement = 'C';
			break;
		}
		complementStrand.insert(complementStrand.begin(), complement);
	}
	return complementStrand;
}