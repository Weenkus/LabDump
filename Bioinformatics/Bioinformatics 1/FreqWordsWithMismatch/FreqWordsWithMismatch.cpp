#include <fstream>
#include <iostream>
#include <string>
#include <vector>
#include <set>

int patternCount(std::string genome, std::string pattern);
void printMaxFreqKmer(std::string genome, int k);
std::set<std::string> neigbours(std::string pattern, int d);
int countMissmatch(const std::string& genome1, const std::string& genome2);

int main() {

	// Open and read the file
	std::ifstream fileHandle("dataset_2_9.txt");
	std::string genome, numb;
	int d{ 0 };

	fileHandle >> genome >> numb >> d;
	int k = stoi(numb);

	printMaxFreqKmer(genome, k, d);

	return 0;
}

int patternCount(std::string genome, std::string pattern) {
	int genLen = genome.length(), patLen = pattern.length(), count{ 0 };
	for (int i{ 0 }; i < genLen - patLen; i++) {
		if (genome.substr(i, patLen).compare(pattern) == 0) {
			++count;
		}
	}
	return count;
}

void printMaxFreqKmer(std::string genome, int k, int d) {
	std::vector<int> kMerCount;
	int genLen = genome.length();

	for (int i{ 0 }; i < genLen - k; i++) {
		kMerCount.push_back(patternCount(genome, genome.substr(i, k)));
	}

	int max{ 0 };
	for (const auto& n : kMerCount) {
		if (n > max)
			max = n;
	}

	int i{ 0 };
	std::vector<std::string> printed;
	std::ofstream myfile;
	myfile.open("output.txt");
	for (const auto& n : kMerCount) {
		if (n == max) {
			if (std::find(printed.begin(), printed.end(), genome.substr(i, k)) != printed.end()) {

			}
			else {
				std::cout << genome.substr(i, k) << " ";
				myfile << genome.substr(i, k) << " ";
			}
			printed.push_back(genome.substr(i, k));
		}
		++i;
	}
	myfile.close();
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