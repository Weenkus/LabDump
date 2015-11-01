#include <iostream>
#include <string>
#include <fstream>

#include "RedBlackTree.h"

using namespace std;

// Function heads
vector<int> parseInputFile(const string& fileName);
void exitPrgoram();

int main(int argc, char* argv[]) {

	// Parse the input file
	vector<int> numbers = parseInputFile(argv[1]);

	// Create the red black tree
	RedBlackTree RBTree = RedBlackTree();
	node* root = RBTree.getRoot();
	for (int numb : numbers)
		RBTree.insert(root, numb);

	RBTree.inorderTraversalPrint(RBTree.getRoot());

	exitPrgoram();
	return 0;
}

vector<int> parseInputFile(const string& fileName) {
	ifstream inputFile;
	inputFile.open(fileName);

	// Read the file
	vector<int> parsedNumbers;
	const int expectedNumberOfInputElements{ 5 };
	parsedNumbers.reserve(expectedNumberOfInputElements);


	int counter{ 0 }, fileNumber{ 0 };
	while (inputFile >> fileNumber) {
		parsedNumbers.push_back(fileNumber);
	}

	// Print what was parsed
	cout << "Parsing complet, file contained:\n";
	for (auto numb : parsedNumbers)
		cout << numb << " ";
	cout << endl;

	return parsedNumbers;
}

void exitPrgoram() {
	cout << "\nEnter any input to exit the program.";
	getchar();
}