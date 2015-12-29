#include <string>
#include <vector>
#include <fstream>
#include <iostream>
#include <set>

std::vector<std::string> neigbours(std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {
	// Initialise streams
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");
	std::string pattern;
	int d;

	// Parse input files
	inputHandle >> pattern >> d;
	std::vector<std::string> dNeigbours = neigbours(pattern, d);

	std::vector<std::string> neigboursInTheHood = neigbours(pattern, d);
	std::set<std::string> neigboursInTheHoodDistrinct;
	for (const auto& s : neigboursInTheHood)
		neigboursInTheHoodDistrinct.insert(s);

	for (const auto s : neigboursInTheHoodDistrinct) {
		if(s.size() == pattern.size())
			outputHandle << s << std::endl;
	}
		

	// Deallocate everything
	inputHandle.close();
	outputHandle.close();
	return 0;
}

std::vector<std::string> neigbours(std::string pattern, int d) {
	std::vector<std::string> fourNucleotides = { "A", "C", "G", "T" };
	if (d == 0) {
		std::vector<std::string> tempPattern = { pattern };
		return tempPattern;
	}
	if (pattern.size() == 0) {
		return fourNucleotides;
	}
	std::vector<std::string> negibourhood;
	std::vector<std::string> suffixNegibourhood = neigbours(pattern.substr(1, pattern.size() - 1), d);
	for (auto n : suffixNegibourhood) {
		if (countMissmatch(pattern.substr(1, pattern.size() - 1), n) < d) {
			for (auto b : fourNucleotides) {
				negibourhood.insert(negibourhood.begin(), b);
			}
		}
		else {
			std::string concat = n;
			concat.insert(concat.begin(), pattern.at(0));
			negibourhood.push_back(concat);
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