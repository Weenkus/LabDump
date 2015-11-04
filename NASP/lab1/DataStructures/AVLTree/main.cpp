#pragma once

#include "AVL.h"

#include <vector>
#include <string>
#include <iostream>
#include <stdio.h>
#include <fstream>

std::vector<int> parseInputFile(const std::string& fileName);
int askUserByMessage(std::string printMessage);

int main(int argc, char* argv[])
{
	// Parse the input file
	dataStructures::AVL AVLTree(parseInputFile(argv[1]));

	// Let user choose the operations on the AVL tree
	while (true) {
		int n = askUserByMessage("\n\n*** CHOOSE INPUT ***\n1.Insert\n2.Delete\n3.Height\n4.Print(inorder)\n5.Show levels\n6.Exit\n\n");
		if (n == 1)
			AVLTree.insert(askUserByMessage("Enter any number: "));
		else if (n == 2)
			askUserByMessage("Enter the number to be deleted: ");
		else if (n == 3) 
			printf("The hight of the tree is: %d.\n", AVLTree.treeHight());
		else if (n == 5)
			AVLTree.printPretty();
		else if (n == 6)
			break;
		if (n == 1 || n == 2 || n == 4)
			AVLTree.inorder();
	}
	return 0;
}

std::vector<int> parseInputFile(const std::string& fileName) {
	std::ifstream inputFile;
	inputFile.open(fileName);

	// Read the file
	std::vector<int> parsedNumbers;
	const int expectedNumberOfInputElements{ 5 };
	parsedNumbers.reserve(expectedNumberOfInputElements);


	int counter{ 0 }, fileNumber{ 0 };
	while (inputFile >> fileNumber) {
		parsedNumbers.push_back(fileNumber);
	}

	// Print what was parsed
	printf("Parsing complet, file contained:\n");
	for (auto& numb : parsedNumbers)
		printf("%d ",numb);
	printf("\n");

	return parsedNumbers;
}

int askUserByMessage(std::string printMessage) {
	int userInput{ 0 };
	std::cout << printMessage;
	scanf_s("%d", &userInput);
	return userInput;
}