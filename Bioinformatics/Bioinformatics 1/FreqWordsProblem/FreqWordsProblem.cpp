#include <fstream>
#include <iostream>
#include <string>
#include <vector>

int patternCount(std::string genome, std::string pattern);
void printMaxFreqKmer(std::string genome, int k);

int main() {

	// Open and read the file
	std::ifstream fileHandel("dataset_2_9.txt");
	std::string genome, numb;

	std::getline(fileHandel, genome);
	std::getline(fileHandel, numb);
	int k = stoi(numb);

	printMaxFreqKmer(genome, k);

	getchar();
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

void printMaxFreqKmer(std::string genome, int k) {
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
		if (n == max ) {
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