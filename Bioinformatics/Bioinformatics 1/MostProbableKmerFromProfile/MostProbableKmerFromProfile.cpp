#include <vector>
#include <set>
#include <string>
#include <iostream>
#include <fstream>
#include <map>

const int NUCLEOTIDE_NUMBER { 4 };
const double MAX_PROBABILTY { 1 };

std::string mostProbableKmerFromProfile(std::string genome, int k, std::vector<std::vector<double>> profile);
double calculateStringProbabilty(std::string genome, std::vector<std::vector<double>> profile);

int main() {
	// Create file handles
	std::ifstream input("input.txt");
	std::ofstream output("output.txt");

	// Parse input file
	std::string genome;
	input >> genome;
	
	int k{ 0 };
	input >> k;

	std::vector<std::vector<double>> profile(k);
	//profile.reserve(k * NUCLEOTIDE_NUMBER);

	for (int j{ 0 }; j < NUCLEOTIDE_NUMBER; ++j) {
		for (int i{ 0 }; i < k; ++i) {
			double prob;
			input >> prob;
			profile[i].push_back(prob);
		}
	}


	for (int j{ 0 }; j < NUCLEOTIDE_NUMBER; ++j) {
		for (int i{ 0 }; i < k; ++i) {
			std::cout << profile[i][j] << " ";
		}
		std::cout << std::endl;
	}

	// Calculate the most probable kmer
	output << mostProbableKmerFromProfile(genome, k, profile);

	// Clean up
	input.close();
	output.close();
}

std::string mostProbableKmerFromProfile(std::string genome, int k, std::vector<std::vector<double>> profile) {
	double minProbabilty{ 0 };
	std::string mostProbString;
	for (int i{ 0 }; i <= genome.length() - k; ++i) {
		double currentProb = calculateStringProbabilty(genome.substr(i, k), profile);
		if (minProbabilty < currentProb) {
			mostProbString = genome.substr(i, k);
			minProbabilty = currentProb;
		}
	}
	return mostProbString;
}

double calculateStringProbabilty(std::string kMer, std::vector<std::vector<double>> profile) {
	std::map<char, int> nucleotideToNumber{ { 'A', 0 },{ 'C', 1 },{ 'G', 2 },{ 'T', 3 } };
	double probility{ 1.0 };

	int i{ 0 };
	for (auto c : kMer) {
		//std::cout << i << "," << c << "(" << nucleotideToNumber[c] << ")";
		probility *= profile[i][nucleotideToNumber[c]];
		//std::cout << " == " << profile[i][nucleotideToNumber[c]] << std::endl;
		++i;
	}
	//std::cout << probility;
	//getchar();
	return probility;
}