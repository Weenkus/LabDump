#include <vector>
#include <set>
#include <string>
#include <iostream>
#include <fstream>
#include <map>

const int NUCLEOTIDE_NUMBER{ 4 };
const double MAX_PROBABILTY{ 1 };

std::map<char, int> nucleotideToNumber{ { 'A', 0 },{ 'C', 1 },{ 'G', 2 },{ 'T', 3 } };

std::vector<std::string> greedyMotifSearch(std::vector<std::string> DNAstrings, int k, int t);

std::string mostProbableKmerFromProfile(std::string genome, int k, std::vector<std::vector<double>> profile);
double calculateStringProbabilty(std::string genome, std::vector<std::vector<double>> profile);

std::vector<std::vector<double>> buildProfileMatrix(std::vector<std::string> DNAstrings);
int score(std::vector<std::string> motifs);
void printMatrix(std::vector<std::vector<double>> matrix);

int main() {
	// Create file handles
	std::ifstream input("input.txt");
	std::ofstream output("output.txt");

	// Parse input
	int k{ 0 }, t{ 0 };
	input >> k >> t;

	std::string line;
	std::vector<std::string> DNAstrings;
	while (std::getline(input, line)) {
		if (!line.empty())
			DNAstrings.push_back(line);
	}

	// Run the greedy algorithm
	std::vector<std::string> greedyMotifs = greedyMotifSearch(DNAstrings, k, t);
	for (auto m : greedyMotifs)
		output << m << std::endl;


	// Clean up
	input.close();
	output.close();
	return 0;
}


std::vector<std::string> greedyMotifSearch(std::vector<std::string> DNAstrings, int k, int t) {
	std::vector<std::vector<double>> profile;
	std::vector<std::string> motifs, bestMotifs;

	// Generate the initial best motifs
	for (int i{ 0 }; i < DNAstrings.size(); ++i) {
		bestMotifs.push_back(DNAstrings[i].substr(0, k));
	}

	for (int i{ 0 }; i <= DNAstrings[0].length() - k; ++i) {
		motifs.clear();
		std::string motif1 = DNAstrings[0].substr(i, k);
		motifs.push_back(motif1);

		for (int j{ 1 }; j < t; ++j) {
			profile = buildProfileMatrix(motifs);

			std::string mostPrebMotif = mostProbableKmerFromProfile(DNAstrings[j], k, profile);
			motifs.push_back(mostPrebMotif);
		}

		if (score(motifs) < score(bestMotifs)) {
			bestMotifs = motifs;
		}
	}

	std::cout << score(bestMotifs);


	return bestMotifs;
}

std::string mostProbableKmerFromProfile(std::string genome, int k, std::vector<std::vector<double>> profile) {
	// Find the most probable kMer using the profile matrix
	double minProbabilty{ 0 };
	std::string mostProbString = genome.substr(0, k);
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
	double probility{ 1.0 };

	int i{ 0 };
	for (auto c : kMer) {
		probility *= profile[nucleotideToNumber[c]][i];
		++i;
	}
	return probility;
}

std::vector<std::vector<double>> buildProfileMatrix(std::vector<std::string> motifs) {
	// Create a profile matrix from DNA strings
	int count[NUCLEOTIDE_NUMBER] = { 0 };
	std::vector<std::vector<double>> profileMatrix(NUCLEOTIDE_NUMBER);
	for (int i{ 0 }; i < motifs[0].length(); ++i) {
		for (int j{ 0 }; j < motifs.size(); ++j) {
			count[nucleotideToNumber[motifs[j][i]]]++;
		}

		for (int j{ 0 }; j < NUCLEOTIDE_NUMBER; ++j) {
			profileMatrix[j].push_back((double)count[j] / motifs.size());
			count[j] = 0;
		}
	}

	for (int i{ 0 }; i < profileMatrix[0].size(); ++i) {
		for (int j{ 0 }; j < profileMatrix.size(); ++j) {
			profileMatrix[j][i]++;
		}
	}

	return profileMatrix;
}

int score(std::vector<std::string> motifs) {
	int count[NUCLEOTIDE_NUMBER] = { 0 }, score{ 0 };
	for (int j{ 0 }; j < motifs[0].length(); ++j) {
		// Score the column
		for (int i{ 0 }; i < motifs.size(); ++i) {
			count[nucleotideToNumber[motifs[i][j]]]++;
		}

		// Find the most freq nucleotide
		int max = 0;
		for (int i{ 0 }; i < NUCLEOTIDE_NUMBER; ++i) {
			max = count[i] > max ? count[i] : max;
			count[i] = 0;
		}
		score += (motifs.size() - max);
	}
	return score;
}

void printMatrix(std::vector<std::vector<double>> matrix) {
	for (int i{ 0 }; i < matrix.size(); ++i) {
		for (int j{ 0 }; j < matrix[0].size(); ++j) {
			std::cout << matrix[i][j] << " ";
		}
		std::cout << std::endl;
	}
	std::cout << std::endl;
}