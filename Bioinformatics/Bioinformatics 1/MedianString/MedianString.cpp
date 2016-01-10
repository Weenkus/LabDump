#include <set>
#include <vector>
#include <fstream>
#include <iostream>
#include <string>

#define MAX_DISTANCE 10000

std::string medianString(std::vector<std::string> DNAstrings, int k);

std::set<std::string> neigbours(std::string pattern, int d);
int countMissmatch( std::string genome1,  std::string genome2);
bool patternMatchWithMissmatch(std::string genome, std::string pattern, int d);

int hamingDistanceAllignment( std::string genome1,  std::string genome2);

int main() {
	// Create the file handles
	std::ifstream input("input.txt");
	std::ofstream output("output.txt");

	// Parse the files;
	int k{ 0 };
	input >> k;

	std::string line;
	std::vector<std::string> DNAstrings;
	while (std::getline(input, line)) {
		if (!line.empty()) {
			DNAstrings.push_back(line);
		}
	}

	// Calculate the median string
	output << medianString(DNAstrings, k);

	// Clean up
	input.close();
	output.close();
	return 0;
}

std::string medianString(std::vector<std::string> DNAstrings, int k) {
	int distance{ MAX_DISTANCE };
	std::string medianString;
	std::set<std::string> allKmers = neigbours(DNAstrings[0].substr(0,k), k);
	for (auto s : allKmers) {
		int totalDistance{ 0 };
		for (auto DNA : DNAstrings) {
			totalDistance += hamingDistanceAllignment(DNA, s);

		}
		if (totalDistance < distance) {
			medianString = s;
			distance = totalDistance;
		}
	}
	return medianString;
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

int countMissmatch(std::string genome1, std::string genome2) {
	int count{ 0 }, genLen1 = genome1.size(), genLen2 = genome2.size();
	int lenMin = genLen1 > genLen2 ? genLen2 : genLen1;
	for (int i{ 0 }; i < lenMin; ++i)
		count += genome1.at(i) != genome2.at(i) ? 1 : 0;
	return count;
}

int hamingDistanceAllignment( std::string genome, std::string pattern) {
	int distance{ 0 };

	int minDistance{ MAX_DISTANCE };
	for (int i{ 0 }; i <= genome.length() - pattern.length(); ++i) {
		distance = countMissmatch(genome.substr(i, pattern.length()), pattern);
		if (distance < minDistance)
			minDistance = distance;
	}
	return minDistance;
}