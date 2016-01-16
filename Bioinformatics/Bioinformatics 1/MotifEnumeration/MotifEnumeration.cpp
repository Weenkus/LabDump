#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <set>

std::set<std::string> motifEnumeration(std::vector<std::string> DNAstrings, int k, int d, int lineNum);
std::set<std::string> neigbours(std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

bool patternMatchWithMissmatch(std::string genome, std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {
	// Open file handles
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");

	// Parse the file
	int d{ 0 }, k{ 0 }, lineNum{ 0 };
	inputHandle >> k >> d;

	std::string line;
	std::vector<std::string> DNAstrings;
	while (std::getline(inputHandle, line)) {
		if (!line.empty()) {
			DNAstrings.push_back(line);
			++lineNum;
		}		
	}

	// Get result and print it
	std::set<std::string> motifs = motifEnumeration(DNAstrings, k, d, lineNum);
	for (auto m : motifs)
		outputHandle << m << " ";

	// Clean up
	inputHandle.close();
	outputHandle.close();
	return 0;
}

std::set<std::string> motifEnumeration(std::vector<std::string> DNAstrings, int k, int d, int lineNum) {
	std::set<std::string> patterns;

	// Check every DNAstring
	for (auto DNAstring : DNAstrings) {
		// Loop all k-mesr
		for (int i{ 0 }; i <= DNAstring.length() - k; ++i) {
			std::set<std::string> neighbourHood = neigbours(DNAstring.substr(i, k), d);
			// See if a neighbour
			for (auto neighbour : neighbourHood) {
				int foindInAllDNAstrings{ 0 };
				for (auto DNA : DNAstrings) {
					if (patternMatchWithMissmatch(DNA, neighbour, d) == true) {
						++foindInAllDNAstrings;
						continue;
					}
				}
				if (foindInAllDNAstrings == lineNum)
					patterns.insert(neighbour);
			}

		}
	}
	return patterns;
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

bool patternMatchWithMissmatch(std::string genome, std::string pattern, int d) {
	int genLen = genome.length(), patLen = pattern.length(), count{ 0 };
	for (int i{ 0 }; i <= genLen - patLen; ++i) {
		if (countMissmatch(genome.substr(i, patLen), pattern) <= d)
			return true;
	}
	return false;
}