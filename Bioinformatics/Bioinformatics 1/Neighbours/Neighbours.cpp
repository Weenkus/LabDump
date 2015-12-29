#include <string>
#include <vector>
#include <fstream>
#include <iostream>
#include <set>

std::set<std::string> neigbours(std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {
	// Initialise streams
	std::ifstream inputHandle("input.txt");
	std::ofstream outputHandle("output.txt");
	std::string pattern;
	int d;

	// Parse input files
	inputHandle >> pattern >> d;

	std::set<std::string> neigboursInTheHood = neigbours(pattern, d);

	for (const auto s : neigboursInTheHood) {
			outputHandle << s << std::endl;
	}
		

	// Deallocate everything
	inputHandle.close();
	outputHandle.close();
	return 0;
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
	std::set<std::string> suffixNegibourhood  = neigbours(suffix, d);

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