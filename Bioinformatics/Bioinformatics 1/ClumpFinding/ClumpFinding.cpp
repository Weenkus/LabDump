#include <vector>
#include <iostream>
#include <string>
#include <fstream>
#include <set>

std::set<std::string> findClumps(std::string text, int k, int L, int t);
std::vector<int> computingFreq(std::string text, int k);

long unsigned int patternToNumberNonRecursive(std::string pattern);
std::string numberToPattern(long unsigned int number, int k);

long unsigned int quotient(long unsigned int number, int divider);
int remainder(long unsigned int number, int divider);

char numberToSymbol(int number);
int symbolToNumber(char symbol);

int main() {
	// Open files 
	std::ifstream inputHandle("E-coli.txt");
	std::ofstream outputHandle("output.txt");
	std::string text;
	int L{ 0 }, k{ 0 }, t{ 0 };

	// Parse input
	std::getline(inputHandle, text);
	inputHandle >> k >> L >> t;
	std::cout << "k = " << k << " L = " << L << " t = " << t << std::endl;

	// Calculate the clumps print them
	std::set<std::string> clumps = findClumps(text, k, L, t);
	//for (const auto& s : clumps)
		//outputHandle << s << " ";
	outputHandle << clumps.size();

	// Be nice and close behind yourself
	inputHandle.close();
	outputHandle.close();
	return 0;
}

std::set<std::string> findClumps(std::string text, int k, int L, int t) {
	// Initialise the freq array and clump vector
	int freqLen = pow(4, k) - 1, textLen = text.length();
	std::vector<int> freqArray(freqLen, 0);
	std::set<std::string> clumps;

	std::string decrementKmer, incrementKmer;
	int jDec{ 0 }, jInc{ 0 };
	for (int i{ 0 }; i <= textLen - L; ++i) {
		if (i % 100 == 0) 
			std::cout << i << " (" << textLen - L << ")" << std::endl;
		
		// Calculate the freq array for the first window
		if (i == 0) {
			freqArray = computingFreq(text.substr(0, L), k);

			// Calculate if there are any new clumps in the freq array
			for (int j{ 0 }; j < freqLen; ++j) {
				if (freqArray[j] >= t) {
					clumps.insert(numberToPattern(j, k));
				}
			}
		}
		// Update only the elements that changed in the freq array (L has lost one and gained one number)
		else {
			decrementKmer = text.substr(i - 1, k);
			incrementKmer = text.substr(i + L - k, k);
			jDec = patternToNumberNonRecursive(decrementKmer);
			jInc = patternToNumberNonRecursive(incrementKmer);
			freqArray[jDec]--;
			freqArray[jInc]++;

			if(freqArray[jInc] >= t)
				clumps.insert(numberToPattern(jInc, k));
		}

		
	}
	return clumps;
}

std::vector<int> computingFreq(std::string text, int k) {
	// Initialise the freq array
	std::vector<int> freqs;
	int freqLen = pow(4, k) - 1, textLen = text.length();
	freqs.reserve(freqLen);
	for (int i{ 0 }; i <= freqLen; i++) {
		freqs.push_back(0);
	}

	// Compute the freq array
	std::string pattern;
	int j{ 0 };
	for (int i{ 0 }; i <= textLen - k; i++) {
		pattern = text.substr(i, k);
		j = patternToNumberNonRecursive(pattern);
		freqs[j] = freqs[j] + 1;
	}

	return freqs;
}

long unsigned int patternToNumberNonRecursive(std::string pattern) {
	int patLen = pattern.length();
	long unsigned int count{ 0 };
	for (int i{ 0 }; i < patLen; i++) {
		count += symbolToNumber(pattern.at(i)) * pow(4, patLen - i - 1);
	}
	return count;
}

std::string numberToPattern(long unsigned int number, int k) {
	std::vector<char> pattern;
	if (k == 1) {
		pattern.push_back(numberToSymbol(number));
	}
	else {
		for (int i{ 0 }; i < k; i++) {
			pattern.insert(pattern.begin(), numberToSymbol(remainder(number, 4)));
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

char numberToSymbol(int number) {
	switch (number) {
	case 0:
		return 'A';
	case 1:
		return 'C';
	case 2:
		return 'G';
	case 3:
		return 'T';
	default:
		std::cout << "ERROR IN numberToSymbol function - got = " << number << std::endl;
	}
}

int symbolToNumber(char symbol) {
	switch (symbol) {
	case 'A':
		return 0;
	case 'C':
		return 1;
	case 'G':
		return 2;
	case 'T':
		return 3;
	default:
		std::cout << "ERROR IN symbolToNumber function" << std::endl;
	}
}