import hashlib
import sys
import time
import gc
from multiprocessing import Pool

MD5_LENGTH_BIT = 128
MD5_LENGTH_HEX = 32
HEX_SCALE = 16
BINARY_SCALE = 2
UTF_8 = 'utf-8'

BAND_LENGTH = 16
NUMBER_OF_BANDS = 8

#MODE = 'SPRUT'
MODE = 'LOCAL'
FILE_NAME = 'inputB.txt'


def main():
    start_time = time.time()
    (Q, N, corpus, I, K) = parse_input(FILE_NAME)
    if MODE == 'LOCAL':
        print("Parsing completed.", time.time() - start_time)
    start_time = time.time()
    hashs = generate_signatures(corpus)
    #assert N == len(hashs)
    if MODE == 'LOCAL':
        print("SimHash computed.", time.time() - start_time)
    start_time = time.time()
    candidates = get_candidates_for_similarity(hashs, N)
    if MODE == 'LOCAL':
        print("Candidates dictionary computed.", time.time() - start_time)
    similarity_search(hashs, I, K, candidates)


def simhash(text):
    sh = [0] * MD5_LENGTH_BIT
    words = text.split(" ")[:-1]
    for word in words:
        bit_hash = hex_to_binary(hash_md5(word), MD5_LENGTH_BIT)
        i = 0
        for bit in bit_hash:
            if bit == '1':
                sh[i] += 1
            else:
                sh[i] -= 1
            i += 1

    bit_sh_list = sh_to_bit(sh)
    bit_sh = ''.join(str(v) for v in bit_sh_list)  # Turn list into a string
    #assert len(bit_sh_list) == MD5_LENGTH_BIT
    return bit_sh


def simhash_builder(bit):
    if bit == '1':
        return 1
    else:
        return -1


def hex_to_binary(hex, length):
    hex = bin(int('1'+str(hex.hexdigest()), HEX_SCALE))[3:]  # Don't remove leading zeroes
    while len(hex) < length:
        hex = '0' + hex

    return hex


def sh_to_bit(final_sh):
    i = 0
    for bit in final_sh:
        if int(bit) >= 0:
            final_sh[i] = 1
        else:
            final_sh[i] = 0
        i += 1
    return final_sh


def hash_md5(word):
    return hashlib.md5(word.encode(UTF_8))


def parse_input(input):
    if MODE == 'LOCAL':
        f = open(input, "r")
        lines = f.read().split("\n")
    else:
        lines = sys.stdin.readlines()

    i = 0
    corpus = []
    I = []
    K = []
    Q = 0
    N = 0
    for line in lines:
        if MODE == 'LOCAL'and i % 100 == 0:
            print(i, N)

        if line == '':
            continue;

        if i == 0:
            N = int(line)
        elif 0 < i <= N:
            corpus.append(line)
        elif i == (N + 1):
            Q = int(line)
        elif i > (N + 1):
            chunks = line.split(" ")
            I.append(int(chunks[0]))
            K.append(int(chunks[1]))

        i += 1

    return Q, N, corpus, I, K


def hem_distance(str1, str2):
    return sum(c1 != c2 for c1, c2 in zip(str1, str2))


def generate_signatures(corpus):
    #hashs = []
    with Pool() as pool:
        return pool.map(simhash, corpus)
    #for text in corpus:
    #    hashs.append(simhash(text))
    #return hashs


def similarity_search(hashs, I, K, candidates):
    if MODE == 'LOCAL':
        output_file = open("output.txt", "w+")
    output_string = ''

    for i in range(len(I)):
        if i % 100 == 0 and MODE == 'LOCAL':
            print(i, len(hashs))

        number_of_matches = 0
        source_hash = hashs[I[i]]
        goal_distance = K[i]
        j = -1
        if I[i] not in candidates:
            continue

        possible_matches = candidates[I[i]]
        for hash_id in possible_matches:
            hash = hashs[hash_id]
            j += 1
            if j == I[i]:   # Don't compare the has with itself
                continue

            if hem_distance(source_hash, hash) <= goal_distance:
                number_of_matches += 1

        output_string += str(number_of_matches) + '\n'

    if MODE == 'LOCAL':
        output_file.write(output_string)
    else:
        print(output_string)


def get_candidates_for_similarity(hashs, N):
    candidates = {}
    for band_number in range(NUMBER_OF_BANDS):
        buckets = {}
        gc.collect()
        if MODE == 'LOCAL':
            print(band_number, NUMBER_OF_BANDS)

        for hash_id in range(N):
            hash = hashs[hash_id]
            band = get_band(hash, band_number)  # Int representation of band
            #print(hash)
            #print(hash[band_number * BAND_LENGTH: ((band_number + 1) * BAND_LENGTH)])
            #assert len(hash) == MD5_LENGTH_BIT
            #assert len(hash[band_number * BAND_LENGTH: ((band_number + 1) * BAND_LENGTH)]) == BAND_LENGTH

            hashs_in_buckets = set()
            if band in buckets:
                hashs_in_buckets = buckets[band]
                for hash_from_bucket_id in hashs_in_buckets:
                    candidates.setdefault(hash_id, set()).add(hash_from_bucket_id)
                    candidates.setdefault(hash_from_bucket_id, set()).add(hash_id)
            else:
                hashs_in_buckets = set()

            hashs_in_buckets.add(hash_id)
            buckets[band] = hashs_in_buckets

    return candidates


def get_band(hash, band_number):
    return int(hash[band_number * BAND_LENGTH: ((band_number + 1) * BAND_LENGTH)], 2)


if __name__ == '__main__':
    main()