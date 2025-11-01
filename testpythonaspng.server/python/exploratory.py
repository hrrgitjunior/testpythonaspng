import pandas as pd
import numpy as np

def example_return_columns_dict(filename: str)  -> list[dict[str,str]]:
    return [{"data": "AAA", "title": "AAA"}, {"data": "BBB", "title": "BBB"}] 

def get_columns(filename: str)  -> list[dict[str,str]]:
    explor_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
    explor_columns = explor_df.columns
    dtable_columns = []
    for column in explor_columns:
        dtable_columns.append({"data": column, "title": column})
    return  dtable_columns

def get_columns_type(filename: str)  -> list[dict[str, str]]:
    explor_df = pd.read_csv(filename, encoding='cp1251',  sep=',')
    explor_columns = explor_df.columns
    dtable_columns = []
    for column in explor_columns:
        dtable_columns.append({"column": column, "type": str(explor_df[column].dtype)})
    return  dtable_columns







