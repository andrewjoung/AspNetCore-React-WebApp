import React, { useState } from 'react'
import { TextField, PrimaryButton, DefaultButton } from 'office-ui-fabric-react';
import { Stack, IStackProps, IStackStyles, IStackTokens } from 'office-ui-fabric-react/lib/Stack';
import { ILibraryDto, Address, LibraryDto } from '../../generated/backend';

interface IProps {
    setAddMode: (addMode: boolean) => void;
    handleCreateLibrary: (library: ILibraryDto) => void;
}

const LibraryForm: React.FC<IProps> = ({setAddMode, handleCreateLibrary}) => {

    const stackStyles: Partial<IStackStyles> = { root: { width: 650 }};
    const columnProps: Partial<IStackProps> = {
        tokens: { childrenGap: 15 },
        styles: { root: { width: 300 }}
    }

    const stackTokens: IStackTokens = { childrenGap: 10 };

    const initializeForm = () => {
        return {
            Id: '',
            Name: '',
            LocationAddressLine1: '',
            LocationAddressLine2: '',
            LocationCity: '',
            LocationStateProvince: '',
            LocationZipCode: '',
            LocationCountry: ''
        }
    }

    const [library, setLibrary] = useState(initializeForm);

    const handleInputChange = (event: any) => {
        //console.log(event.target.value);
        const {name, value} = event.target;
        setLibrary({...library, [name]: value})
    }

    const handleSubmit = async () => {
        console.log("Submitted");
        console.log(library);

        if(library.Name !== '' && library.LocationAddressLine1 !== '' && library.LocationCity !== ''
            && library.LocationCountry !== '' && library.LocationZipCode !== '') {

                let addressItem = {
                    locationAddressLine1: library.LocationAddressLine1,
                    locationAddressLine2: library.LocationAddressLine2,
                    locationCity: library.LocationCity,
                    locationStateProvince: library.LocationStateProvince,
                    locationZipCode: library.LocationZipCode,
                    locationCountry: library.LocationCountry
                }
        
                let newAddress = Address.fromJS(addressItem);
                console.log("New address: ");
                console.log(newAddress);
        
                let libraryItem:ILibraryDto = {
                    id: Math.floor(Math.random() * 10000),
                    name: library.Name,
                    address: newAddress,
                    createdDate: new Date(),
                    updatedDate: new Date()
                }
        
                //console.log("Library item " + libraryItem.name);
        
                let newLibrary = LibraryDto.fromJS(libraryItem);
        
                console.log(newLibrary);
                
                
                handleCreateLibrary(newLibrary);

            
                setAddMode(false);
        } else {
            alert('Please fill out all required fields');
        }

    }

    return(
        <Stack onSubmit={handleSubmit} horizontal tokens={{ childrenGap: 50}} styles={stackStyles}>
            <Stack {... columnProps}>
                <TextField 
                    onChange={handleInputChange} 
                    name="Name" value={library.Name} 
                    label="Library Name" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationAddressLine1" 
                    value={library.LocationAddressLine1} 
                    label="Street Address" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationAddressLine2" 
                    value={library.LocationAddressLine2} 
                    label="Street Address 2" 
                />
                <TextField 
                    onChange={handleInputChange}
                    name="LocationCity" 
                    value={library.LocationCity} 
                    label="City" 
                    required
                />
            </Stack>   

            <Stack {... columnProps}>
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationStateProvince" 
                    value={library.LocationStateProvince} 
                    label="State" 
                    required
                />
                <TextField 
                    onChange={handleInputChange} 
                    name="LocationZipCode" 
                    value={library.LocationZipCode} 
                    label="Zip Code" 
                    required
                />
                <TextField  
                    onChange={handleInputChange}
                    name="LocationCountry" 
                    value={library.LocationCountry} 
                    label="Country" 
                    required
                />
                <Stack horizontal tokens={stackTokens}>
                    <PrimaryButton type="submit" onClick={handleSubmit} text="Submit" />
                    <DefaultButton onClick={() => {setAddMode(false)}} text="Cancel" />
                </Stack>
            </Stack>
        </Stack>
    )

}

export default LibraryForm;