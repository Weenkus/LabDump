import sys
import math
import itertools
import time
import array

MODE = "SPRUT"
#MODE = "LOCAL"

FILE_PATH = 'R.in'


def main():
    start = time.time()
    number_of_baskets, threshold, number_of_buckets, baskets = parse_input()
    if MODE == "LOCAL":
        print('Number of baskets: ', number_of_baskets)
        print('Treshold: ', threshold)
        print('Number of buckets: ', number_of_buckets)
        assert number_of_baskets == len(baskets)

    PCY(number_of_baskets, threshold, number_of_buckets, baskets)
    
    if MODE == 'LOCAL':
        print(time.time()-start)


def parse_input():
    if MODE == 'LOCAL':
        input_file = open(FILE_PATH, "r")
        lines_from_file = input_file.read().split("\n")
    else:
        lines_from_file = sys.stdin.read().split("\n")

    number_of_baskets = int(lines_from_file[0])
    threshold = int(math.floor(float(lines_from_file[1]) * number_of_baskets))
    number_of_buckets = int(lines_from_file[2])
    baskets = []
    for basket in lines_from_file[3:-1]:    # Skip initial parameters and last empty line
        baskets.append(list(map(int, basket.split(" "))))

    return number_of_baskets, threshold, number_of_buckets, baskets


def PCY(number_of_baskets, threshold, number_of_buckets, baskets):
    if MODE == 'LOCAL':
        start = time.time()

    product_count = {}
    for basket in baskets:
        for product in basket:
            product_count[product] = product_count.get(product, 0) + 1

    if MODE == 'LOCAL':
        print('First run: ', time.time()-start)
        start = time.time()

    product_count_length = len(product_count)
    buckets = [0] * number_of_buckets

    for basket in baskets:
        for index_i, i in enumerate(basket):
            for j in basket[index_i+1:]:
                if product_count[i] >= threshold and product_count[j] >= threshold:
                    k = ((i * product_count_length) + j) % number_of_buckets
                    buckets[k] += 1

    if MODE == 'LOCAL':
        print('Second run: ', time.time()-start)
        start = time.time()

    pairs = {}
    for basket in baskets:
        for index_i, i in enumerate(basket):
            for j in basket[index_i+1:]:
                if product_count[i] >= threshold and product_count[j] >= threshold:
                    k = ((i * product_count_length) + j) % number_of_buckets
                    if buckets[k] >= threshold:
                        key = str(i) + "," + str(j)
                        pairs[key] = pairs.get(key, 0) + 1

    m = len([v for v in product_count.values() if v >= threshold])
    A = int(m*(m-1)/2)

    if MODE == 'LOCAL':
        print('Third run: ', time.time()-start)
        start = time.time()

    print(A)
    print(len(pairs))
    for value in sorted(pairs.values(), reverse=True):
        print(value)

    if MODE == 'LOCAL':
        print('Sort: ', time.time()-start)


if __name__ == "__main__":
    main()
