#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <time.h>
#include <map>

const int NUCLEOTIDE_NUMBER{ 4 };
const double MAX_PROBABILTY{ 1 };

std::map<char, int> nucleotideToNumber{ { 'A', 0 },{ 'C', 1 },{ 'G', 2 },{ 'T', 3 } };

std::vector<std::string> randomizedMotifSearch(std::vector<std::string>, int d, int t);

std::string mostProbableKmerFromProfile(std::string genome, int k, std::vector<std::vector<double>> profile);
double calculateStringProbabilty(std::string kMer, std::vector<std::vector<double>> profile);
std::vector<std::vector<double>> buildProfileMatrix(std::vector<std::string> motifs);
int score(std::vector<std::string> motifs);
void printMatrix(std::vector<std::vector<double>> matrix);


int main() {
	// Create file handles
	std::ifstream input("input.txt");
	std::ofstream output("output.txt");

	// Parse
	int k{ 0 }, t{ 0 };
	std::vector<std::string> DNAs;
	std::string line;
	
	while (std::getline(input, line)) {
		if (!line.empty())
			DNAs.push_back(line);
	}

	// Run the algorithm
	std::vector<std::string> bestMotifs = randomizedMotifSearch(DNAs, k, t);
	
	for (auto s : bestMotifs)
		output << s << std::endl;

	// Clean up
	input.close();
	output.close();
	return 0;
}

std::vector<std::string> randomizedMotifSearch(std::vector<std::string> DNAs, int k, int t) {
	srand((unsigned)time(NULL));

	// Randomly select k-mer motif
	std::vector<std::string> motifs, bestMotifs;
	for (auto dna : DNAs) {
		int randNumber = rand() % (dna.length() - k + 1);
		bestMotifs.push_back(dna.substr(randNumber, k));
	}
	motifs = bestMotifs;

	// Run the core algorithm
	std::vector<std::vector<double>> profile;
	do {
		profile = buildProfileMatrix(motifs);

		// Generate new motifs
		std::vector<std::string> newMotifs;
		for (auto dna : DNAs) {
			newMotifs.push_back(mostProbableKmerFromProfile(dna, k, profile));
		}
		motifs = newMotifs;

		// Value the results
		if (score(motifs) < score(bestMotifs)) {
			bestMotifs = motifs;
		}
		else {
			return bestMotifs;
		}

	} while (true);
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
